using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingProvider.Core;
using BillingProvider.Core.KKMDrivers;
using BillingProvider.Core.Parsers;
using BillingProvider.WinForms.Extensions;
using NLog;

namespace BillingProvider.WinForms
{
    public partial class MainWindow : Form
    {
        private AppSettings _appSettings;
        private static Logger _log;

        // private ServerConnection _conn;
        private IKkmDriver _conn;

        private readonly StringBuilder _sbLog;
        private string _filePath;
        private bool _logDirty;
        private FileSystemWatcher _watcher;
        private bool _isWatching;
        private string _folderName;
        private FileStorage _storage;


        public MainWindow()
        {
            InitializeComponent();
            _sbLog = new StringBuilder();
            _logDirty = false;
            _isWatching = false;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // TestCheckToolStripMenuItem.Enabled = false;
            // KktStateToolStripMenuItem.Enabled = false;
            // DeviceListToolStripMenuItem.Enabled = false;

            _log = LogManager.GetCurrentClassLogger();

            _appSettings = new AppSettings();
            gridSettings.SelectedObject = _appSettings;
            if (_appSettings.KkmDriver == AppSettings.KkmDrivers.atol)
            {
                _conn = new AtolOnlineDriver(_appSettings.AtolHost, _appSettings.AtolOnlineINN,
                    _appSettings.AtolOnlineGroupID,
                    _appSettings.AtolOnlineLogin, _appSettings.AtolOnlinePassword, _appSettings.CashierName,
                    _appSettings.CashierVatin, _appSettings.AtolOnlineHostname, _appSettings.CompanyMail);
            }
            else if (_appSettings.KkmDriver == AppSettings.KkmDrivers.kkmserver)
            {
                _conn = new KkmServerDriver(_appSettings.CashierName, _appSettings.CashierVatin,
                    _appSettings.ServerPassword, _appSettings.ServerLogin, _appSettings.ServerAddress,
                    _appSettings.ServerPort, _appSettings.CompanyMail);
            }


            _log.Debug("MainWindow loaded");
            CreateToolStripMenuItem_Click(sender, e);
            _log.Info("Приложение запущено!");
            _storage = new FileStorage(@"history.txt");
            _storage.Load();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(OpenToolStripMenuItem)} clicked");

            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            _log.Info($"Выбран файл: {openFileDialog.FileName}");
            var dt = new DataTable();
            gridSource.DataSource = dt;

            IParser parser;
            try
            {
                parser = ParserSelector.Select(openFileDialog.FileName);
                parser.Load();
                Text = $@"{openFileDialog.FileName} - Billing Provider";
                _log.Debug($"Добавление колонок в {nameof(gridSource)}");
                foreach (var caption in parser.Captions)
                {
                    dt.Columns.Add(caption, typeof(string));
                }

                gridSource.Update();

                _log.Debug($"Добавление данных в {nameof(gridSource)}");
                foreach (var node in parser.Data)
                {
                    dt.LoadDataRow(node.AsArray(), LoadOption.Upsert);
                }

                gridSource.Update();
            }
            catch
            {
                _log.Error($"Не удалось открыть файл: {openFileDialog.FileName}");
                Text = @"Billing Provider";
            }
        }

        private bool _changed;
        private bool _processing;

        private void gridSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _changed = true;
            if (_appSettings.KkmDriver == AppSettings.KkmDrivers.atol)
            {
                _conn = new AtolOnlineDriver(_appSettings.AtolHost, _appSettings.AtolOnlineINN,
                    _appSettings.AtolOnlineGroupID,
                    _appSettings.AtolOnlineLogin, _appSettings.AtolOnlinePassword, _appSettings.CashierName,
                    _appSettings.CashierVatin, _appSettings.AtolOnlineHostname, _appSettings.CompanyMail);
            }
            else if (_appSettings.KkmDriver == AppSettings.KkmDrivers.kkmserver)
            {
                _conn = new KkmServerDriver(_appSettings.CashierName, _appSettings.CashierVatin,
                    _appSettings.ServerPassword, _appSettings.ServerLogin, _appSettings.ServerAddress,
                    _appSettings.ServerPort, _appSettings.CompanyMail);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _log.Debug($"Form closing");

            if (!_changed && !_processing)
            {
                return;
            }

            if (_processing)
            {
                _log.Debug($"Form closing: processing = true");
                MessageBox.Show(@"Идет обработка позиций!");
                e.Cancel = true;
                return;
            }


            if (!_changed)
            {
                return;
            }

            _log.Debug($"Form closing: changed = true");

            var result = MessageBox.Show(@"Вы изменили настройки, сохранить изменения?",
                @"Сохранить?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    _appSettings.UpdateSettings();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void PingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(PingToolStripMenuItem)} clicked");
            // Utils.ServerAvailable(_appSettings.ServerAddress, _appSettings.ServerPort);
            _conn.TestConnection();
        }

        private void TestCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(TestCheckToolStripMenuItem)} clicked");
            _conn.RegisterTestCheck();
        }

        private void KktStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(KktStateToolStripMenuItem)} clicked");
            // _conn.GetDataKkt();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(SaveToolStripMenuItem)} clicked");
            _appSettings.UpdateSettings();
            _changed = false;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Белый Н. С.\nbeliy_ns@kuzro.ru", @"О программе");
        }

        private void DeviceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(DeviceListToolStripMenuItem)} clicked");
            // _conn.List();
        }

        private async void FiscalAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(FiscalAllToolStripMenuItem)} clicked");
            _processing = true;


            for (var i = 0; i < gridSource.RowCount - 1; i++)
            {
                var currentRow = gridSource.Rows[i];
                _log.Debug(
                    $"Current row: {nameof(gridSource)}.Rows[{i}]: {currentRow.Cells[3].Value}, {currentRow.Cells[2].Value}");

                Utils.ChangeBackground(currentRow, Color.PaleGoldenrod);

                try
                {
                    _conn.RegisterCheck(currentRow.Cells[0].Value.ToString(), currentRow.Cells[3].Value.ToString(),
                        currentRow.Cells[2].Value.ToString(), string.Empty);

                    await Task.Delay(1500);

                    Utils.ChangeBackground(currentRow, Color.YellowGreen);
                }
                catch
                {
                    Utils.ChangeBackground(currentRow, Color.Salmon);
                }
            }

            _processing = false;
            _log.Info("Фискализация позиций завершена");
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = @"Billing Provider";

            var dt = new DataTable();
            gridSource.DataSource = dt;

            _log.Debug($"Добавление колонок в {nameof(gridSource)}");
            var parser = new HtmlKbbParser("");

            foreach (var caption in parser.Captions)
            {
                dt.Columns.Add(caption, typeof(string));
            }

            gridSource.Update();
        }

        private void rtxtLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
            _log.Debug($"{e.LinkText} clicked");
        }

        private void WatchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isWatching)
            {
                _isWatching = false;
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                WatchFolderToolStripMenuItem.Text = @"Отслеживать папку";
                _log.Info($"Отслеживание папки заверешено {_folderName}");
            }
            else
            {
                try
                {
                    _folderName = _appSettings.FolderPath;
                    if (!Directory.Exists(_folderName))
                    {
                        _log.Error($"Папка {_folderName} не найдена");
                    }

                    _isWatching = true;
                    WatchFolderToolStripMenuItem.Text = @"Прекратить отслеживание";


                    _watcher = new FileSystemWatcher
                    {
                        Filter = "*.*",
                        Path = _folderName,
                        IncludeSubdirectories = _appSettings.IncludeSubfolders,
                        NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                        EnableRaisingEvents = true
                    };


                    _watcher.Created += OnCreated;

                    _log.Info($"Начато отслеживание папки {_folderName}");
                }
                catch (Exception exception)
                {
                    _log.Error(exception, $"Невозможно отслеживание папки {_folderName}");
                    _isWatching = false;
                    if (_watcher != null)
                    {
                        _watcher.EnableRaisingEvents = false;
                        _watcher.Dispose();
                    }

                    WatchFolderToolStripMenuItem.Text = @"Отслеживать папку";
                    _log.Info($"Отслеживание папки заверешено {_folderName}");
                }
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (_logDirty)
            {
                return;
            }

            _sbLog.Remove(0, _sbLog.Length);
            _sbLog.Append(e.ChangeType.ToString());
            _sbLog.Append(" ");
            _sbLog.Append(e.FullPath);
            _filePath = e.FullPath;


            _logDirty = true;
        }

        private async void tmrEditNotify_Tick(object sender, EventArgs e)
        {
            if (!_logDirty)
            {
                return;
            }

            _log.Info(_sbLog.ToString());
            _logDirty = false;

            string hash;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_filePath))
                {
                    hash = BitConverter.ToString(md5.ComputeHash(stream))
                        .Replace("-", "")
                        .ToLowerInvariant();
                }
            }

            _log.Info($"Hash of {_filePath}: {hash}");
            if (_storage.IsExists(hash))
            {
                _log.Info($"Файл {_filePath} уже обработан");
                return;
            }

            try
            {
                var parser = ParserSelector.Select(_filePath);
                await Task.Run(() => parser.Load());
                foreach (var node in parser.Data)
                {
                    _log.Debug($"Parsing current row: {node.Name}, {node.Sum}");
                    _taskQueue.Enqueue(() => ExecuteTask(node.Name, string.Empty, node.Sum, _filePath));
                }

                if (!tmrQueue.Enabled)
                {
                    tmrQueue.Start();
                }
            }
            catch
            {
                _log.Error($"Не удалось открыть файл: {_filePath}");
            }
        }

        private void ExecuteTask(string clientInfo, string name, string sum, string filePath)
        {
            _log.Debug($"Current row: {clientInfo}, {name}, {sum}");

            try
            {
                _conn.RegisterCheck(clientInfo, name, sum, filePath);
            }
            catch
            {
                _log.Warn($"Строку с {name}, {sum} не удалось обработать");
            }
        }

        private readonly Queue<Action> _taskQueue = new Queue<Action>();

        private void tmrQueue_Tick(object sender, EventArgs e)
        {
            if (_taskQueue.Count == 0)
            {
                tmrQueue.Stop();
                return;
            }

            Task.Factory.StartNew(_taskQueue.Dequeue());
            _log.Info($"Позиций в очереди: {_taskQueue.Count}");
        }

        private async void ScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_appSettings.FolderPath))
            {
                _log.Error($"Папка {_appSettings.FolderPath} не найдена");
                return;
            }

            foreach (var file in Directory.EnumerateFiles(_appSettings.FolderPath, "*.*", SearchOption.AllDirectories))
            {
                _log.Info($"Select file: {file}");

                if (!File.Exists(file))
                {
                    _log.Error($"Файд {file} не найден");
                    continue;
                }

                string hash;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(file))
                    {
                        hash = BitConverter.ToString(md5.ComputeHash(stream))
                            .Replace("-", "")
                            .ToLowerInvariant();
                    }
                }

                _log.Info($"Hash of {file}: {hash}");
                if (_storage.IsExists(hash))
                {
                    _log.Info($"Файл {file} уже обработан");
                    continue;
                }

                try
                {
                    var parser = ParserSelector.Select(file);
                    await Task.Run(() => parser.Load());
                    foreach (var node in parser.Data)
                    {
                        _log.Debug($"Parsing current row: {node.Name}, {node.Sum}");
                        _taskQueue.Enqueue(() => ExecuteTask(node.Name, string.Empty, node.Sum, file));
                    }

                    if (!tmrQueue.Enabled)
                    {
                        tmrQueue.Start();
                    }

                    _storage.AddNode(hash);
                }
                catch
                {
                    _log.Error($"Не удалось открыть файл: {_filePath}");
                }
            }
        }
    }
}