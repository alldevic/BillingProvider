using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
                        var row = result[i].ItemArray;
                        var rowStr = row[0].ToString();

                        if (string.IsNullOrEmpty(rowStr) || (rowStr[0] == '='))
                        {
                            continue;
                        }

                        var tmp = new ClientInfo
                        {
                            Source = string.Join(";", row.Where(o => o is string).ToArray()),
                            Address = row[7].ToString(),
                            Name = !string.IsNullOrEmpty(row[6].ToString()) ? row[6].ToString() : row[5].ToString()
                        };

                        var j = 10;
                        var rawSum = 0m;
                        if (string.Equals(row[j].ToString(), "[!]") && string.Equals(row[j - 1].ToString(), "[!]"))
                        {
                            tmp.Positions.Add(new Position {Name = "Вывоз ТКО", Sum = row[j + 3].ToString()});
                            rawSum = decimal.Parse(row[j + 3].ToString());
                        }
                        else
                        {
                            while (true)
                            {
                                Log.Debug($"Read position: '{row[j + 1]}; {row[j + 2]}'");
                                var posSum = row[j + 2].ToString();
                                var posName = row[j + 1].ToString();
                                if (string.IsNullOrEmpty(posSum) || 
                                    string.Equals(posSum.Replace(",", "."), "0.00") ||
                                    string.IsNullOrEmpty(posName) ||
                                    string.Equals(posName, "ГОСПОШЛИНА")
                                    )
                                {
                                    if (row[j + 3].ToString() == "[!]")
                                    {
                                        break;
                                    }

                                    j += 3;
                                    continue;
                                }

                                
                                tmp.Positions.Add(new Position {Name = posName, Sum = posSum});

                                if (row[j + 3].ToString() == "[!]")
                                {
                                    break;
                                }

                                j += 3;
                            }

                            rawSum = decimal.Parse(row[j + 5].ToString());
                        }


                        var tmpSum = 0m;


                        foreach (var position in tmp.Positions)
                        {
                            // if (string.Equals(position.Name, @"ГОСПОШЛИНА") ||
                            //     string.Equals(position.Name, @"PEN") ||
                            //     string.Equals(position.Name, @"ПЕНЯ"))
                            // {
                            //     continue;
                            // }

                            tmpSum += decimal.Parse(position.Sum.Replace(".", ","));
                            position.Sum = position.Sum.Replace(",", ".");
                        }


                        tmp.Sum = tmpSum.ToString(CultureInfo.InvariantCulture);

                        Data.Add(tmp);

                        if (tmpSum != rawSum)
                        {
                            Log.Warn($"Сумма не совпадает: {tmpSum}!={rawSum}\n{tmp.Source}");
                        }
                    }
                }
            }

            Log.Debug("End specsber parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}