using System.Collections.Generic;
using System.IO;
using System.Text;
using BillingProvider.Core.Models;
using ExcelDataReader;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class SpecSberParser : IParser

    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }

        public SpecSberParser(string path)
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
            Log.Debug("Begin specsber parsing");
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
                        Log.Debug($"{x[5]}; {x[6]}; {x[7]}; Вывоз ТКО; {x[14]}; {x[16]}; {x[19]}");

                        var sum = x[16].ToString();

                        if (string.Equals(x[14].ToString(), @"ПЕНЯ"))
                        {
                            sum = x[19].ToString();
                        }

                        if (string.IsNullOrEmpty(sum))
                        {
                            continue;
                        }

                        var name = x[6].ToString();
                        if (string.IsNullOrEmpty(name))
                        {
                            name = x[5].ToString();
                        }

                        var tmp = new ClientInfo
                        {
                            Address = x[7].ToString(),
                            Name = name,
                        };
                        tmp.Positions.Add(new Position
                        {
                            Name = "Вывоз ТКО",
                            Sum = sum.Replace(",", ".")
                        });

                        tmp.Sum = sum.Replace(",", ".");

                        Data.Add(tmp);
                    }
                }
            }

            Log.Debug("End specsber parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}