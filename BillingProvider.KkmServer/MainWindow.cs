using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillingProvider.Core;
using BillingProvider.Core.Parsers;
using NLog;
using NLog.Fluent;

namespace BillingProvider.KkmServer
{
    public partial class MainWindow : Form
    {
        private AppSettings _appSettings;
        private static Logger _log;

        private ServerConnection _conn;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            TestCheckToolStripMenuItem.Enabled = false;
            KktStateToolStripMenuItem.Enabled = false;
            DeviceListToolStripMenuItem.Enabled = false;

            _log = LogManager.GetCurrentClassLogger();
            _appSettings = new AppSettings();
            gridSettings.SelectedObject = _appSettings;
            _conn = new ServerConnection(_appSettings.ServerPort, _appSettings.ServerAddress,
                _appSettings.ServerLogin, _appSettings.ServerPassword, _appSettings.CashierName,
                _appSettings.CashierVatin);
            _log.Debug("MainWindow loaded");
            CreateToolStripMenuItem_Click(sender, e);
            _log.Info("Приложение запущено!");
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
            Text = $@"{openFileDialog.FileName} - Billing Provider";

            var parser = ParserSelector.Select(openFileDialog.FileName);
            try
            {
                parser.Load();
            }
            catch
            {
                Log.Error($"Не удалось открыть файл: {openFileDialog.FileName}");
            }


            var dt = new DataTable();
            gridSource.DataSource = dt;

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
                gridSource.Update();
            }
        }

        private bool _changed;
        private bool _processing;

        private void gridSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _changed = true;
            _conn = new ServerConnection(_appSettings.ServerPort, _appSettings.ServerAddress,
                _appSettings.ServerLogin, _appSettings.ServerPassword, _appSettings.CashierName,
                _appSettings.CashierVatin);
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


            if (_changed)
            {
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
        }

        private void PingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(PingToolStripMenuItem)} clicked");
            Utils.ServerAvailable(_appSettings.ServerAddress, _appSettings.ServerPort);
        }

        private void TestCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(TestCheckToolStripMenuItem)} clicked");
            _conn.RegisterTestCheck();
        }

        private void KktStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _log.Debug($"{nameof(KktStateToolStripMenuItem)} clicked");
            _conn.GetDataKkt();
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
            _conn.List();
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
                        currentRow.Cells[2].Value.ToString());

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
    }
}