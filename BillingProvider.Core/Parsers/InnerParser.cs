using System.Collections.Generic;
using System.IO;
using System.Linq;
using BillingProvider.Core.Models;
using Newtonsoft.Json;
using NLog;

namespace BillingProvider.Core.Parsers
{
    public class InnerParser : IParser
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public List<ClientInfo> Data { get; }
        public List<string> Captions { get; }
        public string Path { get; }

        public InnerParser(string path)
        {
            Data = new List<ClientInfo>();
            Path = path;
            Captions = new List<string>
            {
                "ФИО", "Адрес", "Сумма", "Позиции",
                "Способ оплаты",
                "Признак способа расчета"
            };
        }

        private class Node
        {
            public string Time { get; set; }
            public string Level { get;  set; }
            public string Message { get; set; }
            public ClientInfo Item { get; set; }
        };

        public void Load()
        {
            Log.Debug("Begin inner parsing");
            var nodes = JsonConvert.DeserializeObject<List<Node>>(File.ReadAllText(Path));
            if (nodes is null)
            {
                return;
            }

            foreach (var node in nodes.Where(node => !string.IsNullOrEmpty(node.Level)))
            {
                node.Item.Address = node.Message;
                Data.Add(node.Item);
            }


            File.WriteAllText(Path, JsonConvert.SerializeObject(nodes, Formatting.Indented));
            Log.Debug("End inner parsing");
            Log.Info($"Файл {Path} успешно загружен");
        }
    }
}