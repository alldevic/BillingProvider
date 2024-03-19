using System.Collections.Generic;
using BillingProvider.Core.Models;
using NLog;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System;

namespace BillingProvider.Core.Parsers
{
    public class TinParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger()
            .WithProperty("ClassName", nameof(TinParser));

        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }
        public PaymentMethod DefaultPaymentMethod { get; }
        public SignMethodCalculation DefaultSignMethodCalculation { get; }

        public TinParser(string path, PaymentMethod paymentMethod, SignMethodCalculation signMethodCalculation)
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
            Log.Debug("Begin Tinkoff parsing");

            using (var parser = new TextFieldParser(Path, Encoding.UTF8))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    var source = parser.ReadLine();
                    var row = source.Split(new[] {';'}, StringSplitOptions.None);

                    Log.Debug($"Read row: '{string.Join("; ", row)}'");

                    if (row[0].StartsWith("="))
                    {
                        continue;
                    }

                    Log.Debug($"Client info: '{row[3].NormalizeJson()}; {row[5]}'");
                    var tmp = new ClientInfo
                    {
                        Source = source,
                        SourcePath = Path,
                        Address = string.Empty,
                        Name = row[3].NormalizeJson(),
                        PaymentMethod = DefaultPaymentMethod,
                        SignMethodCalculation = DefaultSignMethodCalculation
                    };

                    Log.Debug($"Add default position: 'Утилизация ТКО+{row[3]}'");
                    tmp.Positions.Add(new Position
                    {
                        Name = "Вывоз ТКО",
                        Sum = row[5].Replace(",", ".")
                    });

                    Log.Debug($"Read sum: '{row[3]}'");
                    tmp.Sum = row[5].Replace(",", ".");


                    Data.Add(tmp);
                }
            }

            Log.Debug("End Tinkoff parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}