using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;
using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;

namespace BillingProvider.Core.KKMDrivers
{
    public class AtolOnlineDriver : IKkmDriver
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly RestClient _client;
        readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private IKkmDriver _kkmDriverImplementation;

        public AtolOnlineDriver(string atolHost, string inn, string groupId, string login, string password,
            string cashierName,
            string cashierVatin, string hostname, string companyEmail)
        {
            Inn = inn;
            GroupId = groupId;
            Login = login;
            Password = password;
            CashierName = cashierName;
            CashierVatin = cashierVatin;
            Hostname = hostname;
            CompanyEmail = companyEmail;

            _client = new RestClient(atolHost);
        }

        public string Inn { get; }
        public string GroupId { get; }

        public string Address { get; } = string.Empty;
        public int Port { get; } = 0;
        public string Login { get; }
        public string Password { get; }
        public string Hostname { get; }
        public string CashierName { get; }
        public string CashierVatin { get; }

        public string CompanyEmail { get; }

        private string Token { get; set; }
        private DateTime TokenDate { get; set; }

        public async Task<ResponseTaskBase> RegisterCheck(string clientInfo, string name, string sum, string filePath, 
            string source, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                return new ResponseTaskBase()
                {
                    ErrorMessage = "Task was cancelled",
                    ResponseTaskStatus = ResponseTaskStatus.TaskCancelled
                };
            }

            Log.Info($"Регистрация чека: {clientInfo}; {name}; {sum}");

            sum = sum.Replace(".", ",");
            var checkStrings = name.Split(';');
            var tmpStrings = new List<object>();
            foreach (var str in checkStrings)
            {
                try
                {
                    var t = str.Split('+');
                    if (t.Length < 2)
                    {
                        t = new[] {"Вывоз ТКО", sum};
                    }

                    tmpStrings.Add(
                        new
                        {
                            name = t[0],
                            // name = "Вывоз ТКО",
                            price = decimal.Parse(t[1].Replace(".", ",")),
                            quantity = 1.0,
                            sum = decimal.Parse(t[1].Replace(".", ",")),
                            payment_object = "service",
                            payment_method = "full_payment",
                            vat = new
                            {
                                type = "none"
                            }
                        }
                    );
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Неверный формат строки: {clientInfo}; {name}; {sum}");
                    Log.Error($"Исходная строка: `{source}`");
                }
            }

            try
            {
                await UpdateTokenIfNeeded();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new ResponseTaskBase()
                {
                    ErrorMessage = "Task was cancelled",
                    ResponseTaskStatus = ResponseTaskStatus.Failed
                };
            }

            var request = new RestRequest($"{GroupId}/sell", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Token", Token);

            var dt = DateTime.Now;
            try
            {
                if (Regex.IsMatch(filePath, @"\d+\\\d+\\\d+"))
                {
                    var match = Regex.Match(filePath, @"\d+\\\d+\\\d+").ToString().Split('\\');
                    dt = new DateTime(int.Parse(match[0]), int.Parse(match[1]), int.Parse(match[2]), 09, 30, 00);
                }
            }
            catch (Exception e)
            {
                Log.Warn($"Не удалость преобразовать дату из пути: {e.Message}");
            }

            try
            {
                request.AddBody(
                    new
                    {
                        // timestamp = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
                        timestamp = dt.ToString("dd.MM.yyyy hh:mm:ss"),
                        external_id = Guid.NewGuid().ToString("N"),
                        receipt = new
                        {
                            client = new
                            {
                                email = "none",
                                name = clientInfo,
                            },
                            company = new
                            {
                                email = CompanyEmail,
                                sno = "osn",
                                inn = Inn,
                                payment_address = Hostname
                            },
                            items = tmpStrings.ToArray(),
                            cashier = CashierName,
                            payments = new List<object>
                            {
                                new
                                {
                                    type = 1,
                                    sum = decimal.Parse(sum)
                                }
                            },
                            total = decimal.Parse(sum)
                        }
                    }
                );


                var res = await _client.ExecuteTaskAsync<SellResponse>(request, _cancelTokenSource.Token);
                if (!string.IsNullOrEmpty(res.Data?.Uuid))
                {
                    Log.Info($"UUID чека для {clientInfo}: {res.Data?.Uuid}");
                }
                else
                {
                    throw new ArgumentNullException($"UUID не получен\n\n{res.Content}");
                }
                // Log.Info(
                //     $"Ссылка для проверки состояния: https://testonline.atol.ru/possystem/v4/{GroupId}/report/{res.Data.Uuid}?token={Token}");

                if (res?.Data?.Uuid == null)
                {
                    throw new ArgumentNullException("UUID не получени");
                }

                GetOfdUrl(res.Data.Uuid);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Ошибка при получении данных.\n{filePath}\n{source}");
            }

            return new ResponseTaskBase
            {
                ErrorMessage = string.Empty,
                ResponseTaskStatus = ResponseTaskStatus.Complete
            };
        }

        private async void GetOfdUrl(string uuid)
        {
            var req = new RestRequest($"{GroupId}/report/{uuid}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            req.AddHeader("Token", Token);
            await Task.Delay(7500);

            var res0 = await _client.ExecuteTaskAsync<ReportResponse>(req, _cancelTokenSource.Token);
            var url = string.Empty;

            if (!string.IsNullOrEmpty(res0.Data?.Payload))
            {
                var json = JObject.Parse(res0.Data.Payload);
                url = json["ofd_receipt_url"]?.ToString();
            }

            Log.Info($"Ссылка на ОФД ({uuid}): {url}");
        }

        private async Task UpdateTokenIfNeeded()
        {
            if (string.IsNullOrEmpty(Token) || TokenDate <= DateTime.Now.AddHours(25))
            {
                Token = string.Empty;
                var request = new RestRequest("getToken", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddBody(new {login = Login, pass = Password});
                IRestResponse<AuthResponse> res3 = null;
                while (string.IsNullOrEmpty(Token))
                {
                    res3 = await _client.ExecuteTaskAsync<AuthResponse>(request, _cancelTokenSource.Token);
                    Token = res3?.Data?.Token;
                }

                TokenDate = DateTime.Parse(res3?.Data?.Timestamp) + TimeSpan.FromHours(24);
                Log.Info($"Получен токен: {Token}");
            }
        }
        
        public async void RegisterTestCheck()
        {
            RestRequest request;
            if (string.IsNullOrEmpty(Token) || TokenDate <= DateTime.Now)
            {
                request = new RestRequest("getToken", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddBody(new {login = Login, pass = Password});
                var res3 = await _client.ExecuteTaskAsync<AuthResponse>(request, _cancelTokenSource.Token);
                Token = res3.Data.Token;
                TokenDate = DateTime.Parse(res3.Data.Timestamp) + TimeSpan.FromHours(24);
                Log.Info($"Получен токен: {Token}");
            }

            request = new RestRequest($"{GroupId}/sell", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Token", Token);
            request.AddBody(new
            {
                timestamp = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
                external_id = Guid.NewGuid().ToString("N"),
                receipt = new
                {
                    client = new
                    {
                        email = "none",
                        name = "Иванов И. И.",
                    },
                    company = new
                    {
                        email = "test@test.ru",
                        sno = "osn",
                        inn = Inn,
                        payment_address = Hostname
                    },
                    items = new List<object>
                    {
                        new
                        {
                            name = "Тестовая услуга",
                            price = 1.00,
                            quantity = 1.0,
                            sum = 1.00,
                            payment_object = "service",
                            payment_method = "full_payment",
                            vat = new
                            {
                                type = "none"
                            }
                        }
                    },
                    cashier = CashierName,
                    payments = new List<object>
                    {
                        new
                        {
                            type = 1,
                            sum = 1.00
                        }
                    },
                    total = 1.00
                }
            });
            var res = await _client.ExecuteTaskAsync<SellResponse>(request, _cancelTokenSource.Token);
            Log.Info($"UUID тестового чека: {res.Data.Uuid}");
            // Log.Info(
            //     $"Ссылка для проверки состояния: https://testonline.atol.ru/possystem/v4/{GroupId}/report/{res.Data.Uuid}?token={Token}");

            var req = new RestRequest($"{GroupId}/report/{res.Data.Uuid}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            req.AddHeader("Token", Token);
            await Task.Delay(7500);
            var res0 = await _client.ExecuteTaskAsync<ReportResponse>(req, _cancelTokenSource.Token);
            try
            {
                var json = JObject.Parse(res0.Data.Payload);
                var url = json["ofd_receipt_url"];
                Log.Info($"Ссылка на ОФД: {url}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public async void TestConnection()
        {
            var request = new RestRequest("getToken", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddBody(new {login = Login, pass = Password});
            var res = await _client.ExecuteTaskAsync<AuthResponse>(request, _cancelTokenSource.Token);
            Token = res.Data.Token;
            Log.Info($"Получен токен: {Token}");
            Log.Info($"Дата окончания: {res.Data.Timestamp}");
        }

        private class ReportResponse
        {
            public string Payload { get; set; }
        }

        private class AuthResponse
        {
            public string Token { get; set; }
            public string Timestamp { get; set; }
            public object Error { get; set; }
        }

        private class SellResponse
        {
            public string Uuid { get; set; }
            public string Status { get; set; }
            public string Error { get; set; }
            public string Timestamp { get; set; }
        }
    }
}