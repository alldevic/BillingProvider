using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BillingProvider.Core.Models;
using ExcelDataReader;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class TxtMailParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }
        public PaymentMethod DefaultPaymentMethod { get; }
        public SignMethodCalculation DefaultSignMethodCalculation { get; }

        public TxtMailParser(string path, PaymentMethod paymentMethod, SignMethodCalculation signMethodCalculation)
        {
            Data = new List<ClientInfo>();
            Path = path;
            DefaultPaymentMethod = paymentMethod;
            DefaultSignMethodCalculation = signMethodCalculation;
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции",
                "Способ оплаты",
                "Признак способа расчета"
            };
        }

        public void Load()
        {
            Log.Debug("Begin txtmail parsing");
            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration
                       {
                           FallbackEncoding = Encoding.GetEncoding(1251),
                           AutodetectSeparators = new[] {';'}
                       }))
                {
                    var result = reader.AsDataSet().Tables[0].Rows;
                    for (var i = 0; i < result.Count; i++)
                    {
                        var x = result[i];
                        Log.Debug(
                            $"{x[1].ToString().NormalizeJson()}; {x[2].ToString().NormalizeJson()}; Вывоз ТКО; {x[4]}");
                        var tmp = new ClientInfo
                        {
                            Source = string.Join(";", x.ItemArray.Where(o => o is string).ToArray()),
                            SourcePath = Path,
                            Address = x[2].ToString().NormalizeJson(),
                            Name = x[1].ToString().NormalizeJson(),
                            PaymentMethod = DefaultPaymentMethod,
                            SignMethodCalculation = DefaultSignMethodCalculation
                        };
                        tmp.Positions.Add(new Position
                        {
                            Name = "Вывоз ТКО",
                            Sum = x[4].ToString().Replace(",", ".")
                        });

                        tmp.Sum = x[4].ToString().Replace(",", ".");

                        Data.Add(tmp);
                    }
                }
            }

            Log.Debug("End txtmail parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}