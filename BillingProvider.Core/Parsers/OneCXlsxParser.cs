﻿using System;
using System.Collections.Generic;
using System.IO;
using BillingProvider.Core.Models;
using ExcelDataReader;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class OneCXlsxParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }

        public void Load()
        {
            Log.Debug("Begin onecxlsx parsing");
            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet().Tables[0].Rows;
                    for (var i = 1; i < result.Count; i++)
                    {
                        try
                        {
                            var x = result[i];
                            Log.Debug($"{x[0]}, {x[1]}, {x[2]}; {x[3]}; {x[4]}");
                            var tmp = new ClientInfo
                            {
                                Address = $"{x[4]}-{x[3]}",
                                Name = x[3].ToString(),
                            };
                            tmp.Positions.Add(new Position
                            {
                                // Name = x[8].ToString(),
                                Name = "Вывоз ТКО",
                                Sum = x[1].ToString().Replace(",", ".")
                            });

                            Log.Debug($"Read sum: '{x[1]}'");
                            tmp.Sum = x[1].ToString().Replace(",", ".");

                            Data.Add(tmp);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Ошибка при парсинге файла {Path}: {e.Message}");
                        }
                    }
                }
            }

            Log.Debug("End onecxlsx parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }

        public OneCXlsxParser(string path)
        {
            Data = new List<ClientInfo>();
            Path = path;
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции"
            };
        }
    }
}