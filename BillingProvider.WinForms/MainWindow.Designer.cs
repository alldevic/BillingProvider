namespace BillingProvider.WinForms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KktStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WatchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FiscalAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownlaodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitontainer = new System.Windows.Forms.SplitContainer();
            this.gridSettings = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridSource = new System.Windows.Forms.DataGridView();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tmrEditNotify = new System.Windows.Forms.Timer(this.components);
            this.tmrQueue = new System.Windows.Forms.Timer(this.components);
            this.NewTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.MainSplitontainer)).BeginInit();
            this.MainSplitontainer.Panel1.SuspendLayout();
            this.MainSplitontainer.Panel2.SuspendLayout();
            this.MainSplitontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.gridSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.сервисToolStripMenuItem, this.справкаToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(672, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.CreateToolStripMenuItem, this.OpenToolStripMenuItem, this.toolStripSeparator, this.SaveToolStripMenuItem, this.toolStripSeparator1, this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "&Файл";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("CreateToolStripMenuItem.Image")));
            this.CreateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.CreateToolStripMenuItem.Text = "&Создать";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("OpenToolStripMenuItem.Image")));
            this.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.OpenToolStripMenuItem.Text = "&Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(239, 6);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject("SaveToolStripMenuItem.Image")));
            this.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.SaveToolStripMenuItem.Text = "&Сохранить настройки";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.выходToolStripMenuItem.Text = "Вы&ход";
            // 
            // сервисToolStripMenuItem
            // 
            this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.PingToolStripMenuItem, this.DeviceListToolStripMenuItem, this.KktStateToolStripMenuItem, this.TestCheckToolStripMenuItem, this.NewTokenToolStripMenuItem, this.toolStripSeparator4, this.ReportToolStripMenuItem, this.toolStripSeparator6, this.ScanToolStripMenuItem, this.WatchFolderToolStripMenuItem, this.toolStripSeparator2, this.FiscalAllToolStripMenuItem});
            this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
            this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.сервисToolStripMenuItem.Text = "&Сервис";
            // 
            // PingToolStripMenuItem
            // 
            this.PingToolStripMenuItem.Name = "PingToolStripMenuItem";
            this.PingToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.PingToolStripMenuItem.Text = "Ping сервера";
            this.PingToolStripMenuItem.Click += new System.EventHandler(this.PingToolStripMenuItem_Click);
            // 
            // DeviceListToolStripMenuItem
            // 
            this.DeviceListToolStripMenuItem.Name = "DeviceListToolStripMenuItem";
            this.DeviceListToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.DeviceListToolStripMenuItem.Text = "Список устройств";
            this.DeviceListToolStripMenuItem.Click += new System.EventHandler(this.DeviceListToolStripMenuItem_Click);
            // 
            // KktStateToolStripMenuItem
            // 
            this.KktStateToolStripMenuItem.Name = "KktStateToolStripMenuItem";
            this.KktStateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.KktStateToolStripMenuItem.Text = "Состояние ККТ";
            this.KktStateToolStripMenuItem.Click += new System.EventHandler(this.KktStateToolStripMenuItem_Click);
            // 
            // TestCheckToolStripMenuItem
            // 
            this.TestCheckToolStripMenuItem.Name = "TestCheckToolStripMenuItem";
            this.TestCheckToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.TestCheckToolStripMenuItem.Text = "Чек на 0 копеек";
            this.TestCheckToolStripMenuItem.Click += new System.EventHandler(this.TestCheckToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(203, 6);
            // 
            // ReportToolStripMenuItem
            // 
            this.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem";
            this.ReportToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ReportToolStripMenuItem.Text = "Сводка";
            this.ReportToolStripMenuItem.Click += new System.EventHandler(this.ReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(203, 6);
            // 
            // ScanToolStripMenuItem
            // 
            this.ScanToolStripMenuItem.Name = "ScanToolStripMenuItem";
            this.ScanToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ScanToolStripMenuItem.Text = "Пересканировать папку";
            this.ScanToolStripMenuItem.Click += new System.EventHandler(this.ScanToolStripMenuItem_Click);
            // 
            // WatchFolderToolStripMenuItem
            // 
            this.WatchFolderToolStripMenuItem.Name = "WatchFolderToolStripMenuItem";
            this.WatchFolderToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.WatchFolderToolStripMenuItem.Text = "Отслеживать папку";
            this.WatchFolderToolStripMenuItem.Click += new System.EventHandler(this.WatchFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // FiscalAllToolStripMenuItem
            // 
            this.FiscalAllToolStripMenuItem.Name = "FiscalAllToolStripMenuItem";
            this.FiscalAllToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.FiscalAllToolStripMenuItem.Text = "Отправить на кассу";
            this.FiscalAllToolStripMenuItem.Click += new System.EventHandler(this.FiscalAllToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.DownlaodToolStripMenuItem, this.toolStripSeparator3, this.AboutToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // DownlaodToolStripMenuItem
            // 
            this.DownlaodToolStripMenuItem.Name = "DownlaodToolStripMenuItem";
            this.DownlaodToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.DownlaodToolStripMenuItem.Text = "Скачать...";
            this.DownlaodToolStripMenuItem.Click += new System.EventHandler(this.DownlaodToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.AboutToolStripMenuItem.Text = "&О программе...";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // MainSplitontainer
            // 
            this.MainSplitontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitontainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitontainer.Location = new System.Drawing.Point(0, 24);
            this.MainSplitontainer.Name = "MainSplitontainer";
            // 
            // MainSplitontainer.Panel1
            // 
            this.MainSplitontainer.Panel1.Controls.Add(this.gridSettings);
            this.MainSplitontainer.Panel1MinSize = 250;
            // 
            // MainSplitontainer.Panel2
            // 
            this.MainSplitontainer.Panel2.Controls.Add(this.splitContainer2);
            this.MainSplitontainer.Panel2MinSize = 400;
            this.MainSplitontainer.Size = new System.Drawing.Size(672, 462);
            this.MainSplitontainer.SplitterDistance = 261;
            this.MainSplitontainer.SplitterWidth = 3;
            this.MainSplitontainer.TabIndex = 1;
            // 
            // gridSettings
            // 
            this.gridSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSettings.LineColor = System.Drawing.SystemColors.ControlDark;
            this.gridSettings.Location = new System.Drawing.Point(0, 0);
            this.gridSettings.Name = "gridSettings";
            this.gridSettings.Size = new System.Drawing.Size(261, 462);
            this.gridSettings.TabIndex = 0;
            this.gridSettings.ToolbarVisible = false;
            this.gridSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.gridSettings_PropertyValueChanged);
            this.gridSettings.PropertySortChanged += new System.EventHandler(this.gridSettings_PropertySortChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridSource);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtxtLog);
            this.splitContainer2.Size = new System.Drawing.Size(408, 462);
            this.splitContainer2.SplitterDistance = 362;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridSource
            // 
            this.gridSource.AllowUserToResizeRows = false;
            this.gridSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSource.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSource.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSource.Location = new System.Drawing.Point(0, 0);
            this.gridSource.Name = "gridSource";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSource.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridSource.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.gridSource.ShowCellErrors = false;
            this.gridSource.Size = new System.Drawing.Size(408, 362);
            this.gridSource.TabIndex = 0;
            this.gridSource.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSource_CellEndEdit);
            this.gridSource.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridSource_DefaultValuesNeeded);
            this.gridSource.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridSource_EditingControlShowing);
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(408, 97);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtLog_LinkClicked);
            // 
            // tmrEditNotify
            // 
            this.tmrEditNotify.Enabled = true;
            this.tmrEditNotify.Tick += new System.EventHandler(this.tmrEditNotify_Tick);
            // 
            // tmrQueue
            // 
            this.tmrQueue.Interval = 2500;
            this.tmrQueue.Tick += new System.EventHandler(this.tmrQueue_Tick);
            // 
            // NewTokenToolStripMenuItem
            // 
            this.NewTokenToolStripMenuItem.Name = "NewTokenToolStripMenuItem";
            this.NewTokenToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.NewTokenToolStripMenuItem.Text = "Новый токен";
            this.NewTokenToolStripMenuItem.Click += new System.EventHandler(this.NewTokenToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 486);
            this.Controls.Add(this.MainSplitontainer);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(688, 525);
            this.Name = "MainWindow";
            this.Text = "Billing Provider";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.MainSplitontainer.Panel1.ResumeLayout(false);
            this.MainSplitontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.MainSplitontainer)).EndInit();
            this.MainSplitontainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.gridSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem NewTokenToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ReportToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

        private System.Windows.Forms.ToolStripMenuItem DownlaodToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FiscalAllToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid gridSettings;
        private System.Windows.Forms.DataGridView gridSource;
        private System.Windows.Forms.ToolStripMenuItem KktStateToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.SplitContainer MainSplitontainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PingToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScanToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem TestCheckToolStripMenuItem;
        private System.Windows.Forms.Timer tmrEditNotify;
        private System.Windows.Forms.Timer tmrQueue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem WatchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;

        #endregion
    }
}
