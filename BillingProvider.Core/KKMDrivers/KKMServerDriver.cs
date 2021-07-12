using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BillingProvider.Core.Comm.Tasks.Response;
using BillingProvider.Core.Models;
using NLog;
using RestSharp;

namespace BillingProvider.Core.KKMDrivers
{
    public class KkmServerDriver : IKkmDriver
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly RestClient _restClient;

        public KkmServerDriver(string cashierName, string cashierVatin, string password, string login, string address,
            int port, string companyEmail, Vat vat)
        {
            CashierName = cashierName;
            CashierVatin = cashierVatin;
            Password = password;
            Login = login;
            Address = address;
            Port = port;
            CompanyEmail = companyEmail;
            Vat = vat;

            _restClient = new RestClient($"http://{Address}:{Port}");
        }

        public int Port { get; }
        public string Address { get; }
        public string Login { get; }
        public string Password { get; }
        public string CashierName { get; }
        public string CashierVatin { get; }
        public string CompanyEmail { get; }
        public Vat Vat { get; }


        public async Task<ResponseTaskBase> RegisterCheck(string clientInfo, string name, string sum, string filePath,
            string source, CancellationToken ct,
            SignMethodCalculation signMethodCalculation = SignMethodCalculation.FULL_PAYMENT,
            PaymentMethod paymentMethod = PaymentMethod.ElectronicPayment_1081)
        {
            Log.Info($"Регистрация чека: {clientInfo}; {name}; {sum}");

            sum = sum.Replace(",", ".");
            var checkStrings = name.Split(';');
            var tmpStrings = new List<object>();
            foreach (var str in checkStrings)
            {
                var t = str.Split('+');
                tmpStrings.Add(new
                {
                    Register = new
                    {
                        Name = t[0],
                        Quantity = 1,
                        Price = t[1].Replace(",", "."),
                        Tax = Vat,
                        Amount = t[1].Replace(",", "."),
                        SignMethodCalculation = signMethodCalculation,
                        SignCalculationObject = 4
                    }
                });
            }

            var receipt = new
            {
                Command = "RegisterCheck",
                NumDevice = 0,
                IdCommand = Guid.NewGuid().ToString("N"),
                IsFiscalCheck = true,
                TypeCheck = 0,
                NotPrint = false,
                NumberCopies = 0,
                CashierName,
                CashierVATIN = CashierVatin,
                ClientInfo = clientInfo,
                CheckStrings = tmpStrings.ToArray(),
                SenderEmail = CompanyEmail
            };

            var final_receipt = receipt.AsDictionary();
            
            switch (paymentMethod)
            {
                case PaymentMethod.Cash_1031:
                    final_receipt.Add("Cash", sum);
                    break;
                case PaymentMethod.Credit_1216:
                    final_receipt.Add("Credit", sum);
                    break;
                case PaymentMethod.AdvancePayment_1215:
                    final_receipt.Add("AdvancePayment", sum);
                    break;
                case PaymentMethod.CashProvision_1217:
                    final_receipt.Add("CashProvision", sum);
                    break;
                case PaymentMethod.ElectronicPayment_1081:
                    final_receipt.Add("ElectronicPayment", sum);
                    break;
            }

            return await ExecuteCommand(final_receipt);
        }

        public async void RegisterTestCheck(SignMethodCalculation signMethodCalculation, PaymentMethod paymentMethod)
        {
            Log.Info($"Регистрация тестового чека");

            var receipt = new
            {
                Command = "RegisterCheck",
                NumDevice = 0,
                IdCommand = Guid.NewGuid().ToString("N"),
                IsFiscalCheck = true,
                TypeCheck = 0,
                NotPrint = false,
                NumberCopies = 0,
                CashierName,
                CashierVATIN = CashierVatin,
                ClientInfo = CashierName,
                SenderEmail = CompanyEmail,
                CheckStrings = new[]
                {
                    new
                    {
                        Register = new
                        {
                            Name = "Тестовая услуга",
                            Quantity = 1,
                            Price = 0,
                            Tax = Vat,
                            Amount = 0.00,
                            SignMethodCalculation = signMethodCalculation,
                            SignCalculationObject = 4
                        }
                    }
                },
            };

            var final_receipt = receipt.AsDictionary();
            
            switch (paymentMethod)
            {
                case PaymentMethod.Cash_1031:
                    final_receipt.Add("Cash", 0);
                    break;
                case PaymentMethod.Credit_1216:
                    final_receipt.Add("Credit", 0);
                    break;
                case PaymentMethod.AdvancePayment_1215:
                    final_receipt.Add("AdvancePayment", 0);
                    break;
                case PaymentMethod.CashProvision_1217:
                    final_receipt.Add("CashProvision", 0);
                    break;
                case PaymentMethod.ElectronicPayment_1081:
                    final_receipt.Add("ElectronicPayment", 0);
                    break;
            }

            await ExecuteCommand(final_receipt);
        }

        public async void TestConnection()
        {
            var tcpClient = new TcpClient();
            try
            {
                await tcpClient.ConnectAsync(Address, Port);
                Log.Info($"Сервер {Address}:{Port} доступен!");
            }
            catch
            {
                Log.Warn($"Сервер {Address}:{Port} не доступен!");
            }
        }

        public async void GetKktInfo()
        {
            Log.Info($"Получение текущего состояниея КТТ");
            await Task.Run(() => ExecuteCommand(new
            {
                Command = "GetDataKKT",
                NumDevice = 0,
                IdCommand = Guid.NewGuid().ToString("N")
            }));
        }

        public async void List()
        {
            Log.Info("Получение списка устройств");
            await Task.Run(() => ExecuteCommand(new
            {
                Command = "List",
                NumDevice = 0
            }));
        }

        private async Task<ResponseTaskBase> ExecuteCommand(object obj)
        {
            Log.Debug("Begin command execution");
            var request = new RestRequest("Execute/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var bytes = Encoding.UTF8.GetBytes($"{Login}:{Password}");

            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
            request.AddBody(obj);
            Log.Debug($"Request: obj0={request.Parameters?[0]}");
            Log.Debug($"Request: obj1={request.Parameters?[1]}");

            RestResponse<KkmServerResponse> resp;

            try
            {
                resp = _restClient.Execute<KkmServerResponse>(request) as RestResponse<KkmServerResponse>;
            }
            catch (Exception e)
            {
                return new ResponseTaskBase
                {
                    ErrorMessage = e.Message,
                    ResponseTaskStatus = ResponseTaskStatus.Failed
                };
            }

            var response = resp?.Data;

            if (response?.Status == 2 || response?.Status == 3)
            {
                Log.Error(response.Error);
                return new ResponseTaskBase
                {
                    ErrorMessage = response.Error,
                    ResponseTaskStatus = ResponseTaskStatus.Failed
                };
            }

            Log.Info($"{response?.Command}({response?.IdCommand}): запрос обработан успешно");
            Log.Debug(resp?.Content);


            return new ResponseTaskBase
            {
                ErrorMessage = string.Empty,
                ResponseTaskStatus = ResponseTaskStatus.Complete
            };
        }
    }
}