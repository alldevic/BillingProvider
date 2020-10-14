using System.Collections.Generic;
using System.IO;
using System.Text;
using BillingProvider.Core.Models;
using ExcelDataReader;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class SpecSberParserThree : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }

        public SpecSberParserThree(string path)
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
            Log.Debug("Begin specsberthree parsing");
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
                        Log.Debug($"{x[6]}; {x[7]}; Вывоз ТКО; {x[10]}");
                        if (string.IsNullOrEmpty(x[10].ToString()) || string.IsNullOrEmpty(x[6].ToString()))
                        {
                            Log.Warn($"Пропущена строка {Path}: {x[6]}; {x[7]}; Вывоз ТКО; {x[10]}");
                            continue;
                        }

                        var tmp = new ClientInfo
                        {
                            Address = x[7].ToString(),
                            Name = x[6].ToString(),
                        };
                        tmp.Positions.Add(new Position
                        {
                            Name = "Вывоз ТКО",
                            Sum = x[10].ToString().Replace(",", ".")
                        });

                        tmp.Sum = x[10].ToString().Replace(",", ".");

                        Data.Add(tmp);
                    }
                }
            }

            Log.Debug("End specsberthree parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}