﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BillingProvider.Core.Models;
using ExcelDataReader;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class XlsxParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }
        public PaymentMethod DefaultPaymentMethod { get; }
        public SignMethodCalculation DefaultSignMethodCalculation { get; }

        public void Load()
        {
            Log.Debug("Begin xlsx parsing");
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
                            Log.Debug(
                                $"{x[0]}, {x[1]}, {x[2]}; {x[3].ToString().NormalizeJson()}; {x[4].ToString().NormalizeJson()}, {x[8]}; {x[7]}");
                            var tmp = new ClientInfo
                            {
                                Source = string.Join(";", x.ItemArray.Where(o => o is string).ToArray()),
                                SourcePath = Path,
                                Address = $"{x[0]}, дом {x[1]}, кв. {x[2]}",
                                Name = !string.IsNullOrEmpty(x[3].ToString())
                                    ? x[3].ToString().NormalizeJson()
                                    : x[4].ToString().NormalizeJson(),
                                PaymentMethod = DefaultPaymentMethod,
                                SignMethodCalculation = DefaultSignMethodCalculation
                            };
                            tmp.Positions.Add(new Position
                            {
                                // Name = x[8].ToString(),
                                Name = "Вывоз ТКО",
                                Sum = x[7].ToString().Replace(",", ".")
                            });

                            Log.Debug($"Read sum: '{x[7]}'");
                            tmp.Sum = x[7].ToString().Replace(",", ".");

                            Data.Add(tmp);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Ошибка при парсинге файла {Path}: {e.Message}");
                        }
                    }
                }
            }

            Log.Debug("End xlsx parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }

        public XlsxParser(string path, PaymentMethod paymentMethod, SignMethodCalculation signMethodCalculation)
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
    }
}