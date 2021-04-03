using System;
using System.Collections.Generic;
using System.Text;
using BillingProvider.Core.Models;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class CsvSberParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public CsvSberParser(string path)
        {
            Data = new List<ClientInfo>();
            Path = path;
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции"
            };
        }

        public string Path { get; }
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
                    var row = source.Split(new [] {';'}, StringSplitOptions.None);
                    
                    Log.Debug($"Read row: '{string.Join(", ", row)}'");

                    if (row.Length < 10)
                    {
                        continue;
                    }

                    Log.Debug($"Client info: '{row[6]}; {row[7]}'");
                    var tmp = new ClientInfo
                    {
                        Source = source,
                        Address = row[7],
                        Name = row[6]
                    };

                    var i = 10;
                    while (true)
                    {
                        Log.Debug($"Read position: '{row[i + 1]}; {row[i + 2]}'");

                        tmp.Positions.Add(new Position {Name = row[i + 1], Sum = row[i + 2].Replace(",", ".")});

                        if (row[i + 3] == "[!]")
                        {
                            break;
                        }

                        i += 3;
                    }
                    
                    Log.Debug($"Read sum: '{row[i + 5]}'");
                    tmp.Sum = row[i + 5].Replace(",", ".");

                    Data.Add(tmp);
                }
            }

            Log.Debug("End parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}