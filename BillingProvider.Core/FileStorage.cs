using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace BillingProvider.Core
{
    public class FileStorage
    {
        public FileStorage(string path)
        {
            Path = path;
            Storage = new List<string>();
        }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private string Path { get; }
        private List<string> Storage { get; set; }

        public void Load()
        {
            if (System.IO.File.Exists(Path))
            {
                Storage = System.IO.File.ReadAllLines(Path).ToList();
            }
            else
            {
                System.IO.File.Create(Path);
                Storage = new List<string>();
            }

            Log.Info($"Файл с историей {Path} успешно загружен");
        }

        public void Save()
        {
            try
            {
                using (var file = new System.IO.StreamWriter(Path))
                {
                    foreach (var hash in Storage)
                    {
                        file.WriteLine(hash);
                        // Log.Debug($"Файл {hash} успешно записан в историю");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Невозможно записать историю в файл: {e.Message}");
            }
        }

        public void AddNode(string hash)
        {
            if (Storage.All(x => !string.Equals(x, hash)))
            {
                Storage.Add(hash);
                Save();
            }
        }

        public bool IsExists(string hash) =>
            Storage.Any(x => string.Equals(x, hash));
    }
}