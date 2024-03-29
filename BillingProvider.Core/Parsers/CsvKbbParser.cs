using System;
using System.Collections.Generic;
using System.Text;
using BillingProvider.Core.Models;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class CsvKbbParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public CsvKbbParser(string path, PaymentMethod paymentMethod, SignMethodCalculation signMethodCalculation)
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

        public string Path { get; }
        public PaymentMethod DefaultPaymentMethod { get; }
        public SignMethodCalculation DefaultSignMethodCalculation { get; }

        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }

        public void Load()
        {
            Log.Debug("Begin parsing");
            using (var parser = new TextFieldParser(Path, Encoding.Default))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    var source = parser.ReadLine();
                    var row = source.Split(new[] {';'}, StringSplitOptions.None);

                    Log.Debug($"Read row: '{string.Join("; ", row)}'");

                    if (row.Length < 11)
                    {
                        continue;
                    }

                    var name = row[0].NormalizeJson();
                    var address = row[1].NormalizeJson();

                    Log.Debug($"Client info: '{name}; {address}'");
                    var tmp = new ClientInfo
                    {
                        Source = source,
                        SourcePath = Path,
                        Address = address,
                        Name = name,
                        PaymentMethod = DefaultPaymentMethod,
                        SignMethodCalculation = DefaultSignMethodCalculation
                    };

                    Log.Debug($"Add default position: 'Утилизация ТКО+{row[3]}'");
                    tmp.Positions.Add(new Position
                    {
                        Name = "Вывоз ТКО",
                        Sum = row[3].Replace(",", ".")
                    });

                    Log.Debug($"Read sum: '{row[3]}'");
                    tmp.Sum = row[3].Replace(",", ".");


                    Data.Add(tmp);
                }
            }

            Log.Debug("End parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}