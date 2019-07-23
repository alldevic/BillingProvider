using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BillingProvider.Core.BankTransfer;
using BillingProvider.Core.Models;
using NLog;

namespace BillingProvider.Core
{
    public class OneCParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }


        public OneCParser(string path)
        {
            Data = new List<ClientInfo>();
            Path = path;
            Captions = new List<string>()
            {
                "ФИО", "Дата", "Сумма", "Позиции"
            };
        }

        public void Load()
        {
            Log.Debug("Begin parsing");

            var tmp = new BankTransferDocumentParser(Path);
            tmp.Parse();
            var tmp0 = tmp.TransferDocuments.Where(x => x.DocumentType == TransferDocumentType.PaymentDraft)
//                .Where(x => !x.PayerName.Contains("ТСЖ"))
//                .Where(x => !x.PayerName.Contains("ЭкоГрад"))
                .ToList();
            foreach (var document in tmp0)
            {
                Data.Add(new ClientInfo()
                {
                    Name = document.PayerName,
                    Sum = document.Total.ToString(CultureInfo.InvariantCulture),
                    Positions = new List<Position>(new[]
                    {
                        new Position()
                        {
                            Name = document.PaymentPurpose,
                            Sum = document.Total.ToString(CultureInfo.InvariantCulture)
                        },
                    }),
                    Address = document.Date.ToString(CultureInfo.CurrentCulture)
                });
            }

            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}