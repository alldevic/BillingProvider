using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BillingProvider.Core.Models;
using BillingProvider.Core.Parsers.BankTransfer;
using NLog;

namespace BillingProvider.Core.Parsers
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
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции"
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
                Data.Add(new ClientInfo
                {
                    Source = $"{document.PayerName};{document.Total.ToString(CultureInfo.InvariantCulture)};{document.PaymentPurpose}",
                    Name = document.PayerName,
                    Sum = document.Total.ToString(CultureInfo.InvariantCulture),
                    Positions = new List<Position>(new[]
                    {
                        new Position
                        {
                            Name = document.PaymentPurpose,
                            Sum = document.Total.ToString(CultureInfo.InvariantCulture)
                        },
                    }),
                    Address = string.Empty
                });
            }

            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}