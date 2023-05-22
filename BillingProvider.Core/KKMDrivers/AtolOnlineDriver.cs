using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;
using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using BillingProvider.Core.Models;

namespace BillingProvider.Core.KKMDrivers
{
    public class AtolOnlineDriver : IKkmDriver
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly RestClient _client;
        readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

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
            string source, CancellationToken ct,
            SignMethodCalculation signMethodCalculation = SignMethodCalculation.FULL_PAYMENT,
            PaymentMethod paymentMethod = PaymentMethod.ElectronicPayment_1081,
            string authToken = "")
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

            if (string.Equals(sum, "0") || string.Equals(sum, "0.0") || string.Equals(sum, "0.00"))
            {
                Log.Info($"Регистрация чека {clientInfo}; {name}; {sum} прервана: сумма равна 0");
                return new ResponseTaskBase
                {
                    ErrorMessage = string.Empty,
                    ResponseTaskStatus = ResponseTaskStatus.Complete
                };
            }

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

            var request = new RestRequest($"{GroupId}/sell", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Token", authToken);

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
                request.AddJsonBody(
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


                var res = await _client.ExecuteAsync<SellResponse>(request, _cancelTokenSource.Token);
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
                    throw new ArgumentNullException("UUID не получен");
                }

                GetOfdUrl(res.Data.Uuid, authToken);
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

        private async void GetOfdUrl(string uuid, string authToken)
        {
            var req = new RestRequest($"{GroupId}/report/{uuid}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            req.AddHeader("Token", authToken);
            await Task.Delay(7500);

            var res0 = await _client.ExecuteAsync<ReportResponse>(req, _cancelTokenSource.Token);
            var url = string.Empty;

            if (!string.IsNullOrEmpty(res0.Data?.Payload))
            {
                var json = JObject.Parse(res0.Data.Payload);
                url = json["ofd_receipt_url"]?.ToString();
            }

            Log.Info($"Ссылка на ОФД ({uuid}): {url}");
        }

        public async void RegisterTestCheck(SignMethodCalculation signMethodCalculation, PaymentMethod paymentMethod,
            string authToken)
        {
            RestRequest request;

            request = new RestRequest($"{GroupId}/sell", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Token", authToken);
            request.AddJsonBody(new
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
            var res = await _client.ExecuteAsync<SellResponse>(request, _cancelTokenSource.Token);
            Log.Info($"UUID тестового чека: {res.Data.Uuid}");
            Log.Info(
                $"Ссылка для проверки состояния: https://testonline.atol.ru/possystem/v4/{GroupId}/report/{res.Data.Uuid}?token={authToken}");

            var req = new RestRequest($"{GroupId}/report/{res.Data.Uuid}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            req.AddHeader("Token", authToken);
            await Task.Delay(7500);
            var res0 = await _client.ExecuteAsync<ReportResponse>(req, _cancelTokenSource.Token);
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

        public async void TestConnection() => await Task.Delay(100);

        public async void GetKktInfo()
        {
            var client = new RestClient("https://online.atol.ru/api/auth/v1/");
            var request = new RestRequest("gettoken", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(new {login = Login, pass = Password});
            var res = await client.ExecuteAsync<AuthResponse>(request, _cancelTokenSource.Token);
            var token = res.Data.Token;
            Log.Info($"Получен токен: {token}");

            client = new RestClient("https://online.atol.ru/api/kkt/v1/");
            request = new RestRequest("cash-registers", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Token", token);
            request.AddParameter("limit", "100");
            res = await client.ExecuteAsync<AuthResponse>(request, _cancelTokenSource.Token);

            Log.Info(res.Data);
        }

        private class ReportResponse
        {
            public string Payload { get; set; }
        }

        public class AuthResponse
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