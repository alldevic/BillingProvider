using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillingProvider.Core.Models;
using HtmlAgilityPack;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class HtmlKbbParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }

        public HtmlKbbParser(string path)
        {
            Path = path;
            Data = new List<ClientInfo>();
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции"
            };
        }

        public void Load()
        {
            Log.Debug("Begin parsing");

            var doc = new HtmlDocument();
            doc.Load(Path, Encoding.UTF8);

            foreach (var row in doc.DocumentNode.SelectNodes("//tr").Skip(1))
            {
                var data = row.Descendants("td").Select(x => x.InnerText.Trim()).ToList();
                Log.Debug($"Read row: '{string.Join(", ", data)}'");

                Log.Debug($"Client info: '{data[0]}, {data[1]}, {data[2]}; {data[3]}; {data[7]}'");
                var info = new ClientInfo
                {
                    Source = string.Join(";", data.ToArray()),
                    SourcePath = Path,
                    Address = $"{data[0]}, {data[1]}, {data[2]}",
                    Name = data[3],
                    Sum = data[7].Replace(",", ".")
                };

                Log.Debug($"Read position: '{data[8]}; {data[7]}'");
                info.Positions.Add(new Position
                {
                    Name = data[8],
                    Sum = data[7]
                });

                Data.Add(info);
            }

            Log.Debug("End parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}