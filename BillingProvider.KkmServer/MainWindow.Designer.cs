namespace BillingProvider.KkmServer
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.FiscalAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitontainer = new System.Windows.Forms.SplitContainer();
            this.gridSettings = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridSource = new System.Windows.Forms.DataGridView();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.файлToolStripMenuItem, this.сервисToolStripMenuItem, this.справкаToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(784, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.CreateToolStripMenuItem, this.OpenToolStripMenuItem, this.toolStripSeparator,
                this.SaveToolStripMenuItem, this.toolStripSeparator1, this.выходToolStripMenuItem
            });
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "&Файл";
            this.CreateToolStripMenuItem.Image =
                ((System.Drawing.Image) (resources.GetObject("CreateToolStripMenuItem.Image")));
            this.CreateToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.ShortcutKeys =
                ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.CreateToolStripMenuItem.Text = "&Создать";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            this.OpenToolStripMenuItem.Image =
                ((System.Drawing.Image) (resources.GetObject("OpenToolStripMenuItem.Image")));
            this.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys =
                ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.OpenToolStripMenuItem.Text = "&Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(231, 6);
            this.SaveToolStripMenuItem.Image =
                ((System.Drawing.Image) (resources.GetObject("SaveToolStripMenuItem.Image")));
            this.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys =
                ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.SaveToolStripMenuItem.Text = "&Сохранить настройки";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.выходToolStripMenuItem.Text = "Вы&ход";
            this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.PingToolStripMenuItem, this.DeviceListToolStripMenuItem, this.KktStateToolStripMenuItem,
                this.TestCheckToolStripMenuItem, this.toolStripSeparator6, this.FiscalAllToolStripMenuItem
            });
            this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
            this.сервисToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.сервисToolStripMenuItem.Text = "&Сервис";
            this.PingToolStripMenuItem.Name = "PingToolStripMenuItem";
            this.PingToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.PingToolStripMenuItem.Text = "Ping сервера";
            this.PingToolStripMenuItem.Click += new System.EventHandler(this.PingToolStripMenuItem_Click);
            this.DeviceListToolStripMenuItem.Name = "DeviceListToolStripMenuItem";
            this.DeviceListToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.DeviceListToolStripMenuItem.Text = "Список устройств";
            this.DeviceListToolStripMenuItem.Visible = false;
            this.DeviceListToolStripMenuItem.Click += new System.EventHandler(this.DeviceListToolStripMenuItem_Click);
            this.KktStateToolStripMenuItem.Name = "KktStateToolStripMenuItem";
            this.KktStateToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.KktStateToolStripMenuItem.Text = "Состояние ККТ";
            this.KktStateToolStripMenuItem.Visible = false;
            this.KktStateToolStripMenuItem.Click += new System.EventHandler(this.KktStateToolStripMenuItem_Click);
            this.TestCheckToolStripMenuItem.Name = "TestCheckToolStripMenuItem";
            this.TestCheckToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.TestCheckToolStripMenuItem.Text = "Чек на 1 рубль";
            this.TestCheckToolStripMenuItem.Visible = false;
            this.TestCheckToolStripMenuItem.Click += new System.EventHandler(this.TestCheckToolStripMenuItem_Click);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(178, 6);
            this.FiscalAllToolStripMenuItem.Name = "FiscalAllToolStripMenuItem";
            this.FiscalAllToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.FiscalAllToolStripMenuItem.Text = "Отправить на кассу";
            this.FiscalAllToolStripMenuItem.Click += new System.EventHandler(this.FiscalAllToolStripMenuItem_Click);
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.AboutToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.AboutToolStripMenuItem.Text = "&О программе...";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            this.MainSplitontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitontainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitontainer.Location = new System.Drawing.Point(0, 24);
            this.MainSplitontainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MainSplitontainer.Name = "MainSplitontainer";
            this.MainSplitontainer.Panel1.Controls.Add(this.gridSettings);
            this.MainSplitontainer.Panel1MinSize = 250;
            this.MainSplitontainer.Panel2.Controls.Add(this.splitContainer2);
            this.MainSplitontainer.Panel2MinSize = 400;
            this.MainSplitontainer.Size = new System.Drawing.Size(784, 537);
            this.MainSplitontainer.SplitterDistance = 261;
            this.MainSplitontainer.TabIndex = 1;
            this.gridSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSettings.LineColor = System.Drawing.SystemColors.ControlDark;
            this.gridSettings.Location = new System.Drawing.Point(0, 0);
            this.gridSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridSettings.Name = "gridSettings";
            this.gridSettings.Size = new System.Drawing.Size(261, 537);
            this.gridSettings.TabIndex = 0;
            this.gridSettings.PropertyValueChanged +=
                new System.Windows.Forms.PropertyValueChangedEventHandler(this.gridSettings_PropertyValueChanged);
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.gridSource);
            this.splitContainer2.Panel2.Controls.Add(this.rtxtLog);
            this.splitContainer2.Size = new System.Drawing.Size(519, 537);
            this.splitContainer2.SplitterDistance = 387;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            this.gridSource.AllowUserToResizeRows = false;
            this.gridSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridSource.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSource.Location = new System.Drawing.Point(0, 0);
            this.gridSource.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridSource.Name = "gridSource";
            this.gridSource.RowHeadersWidthSizeMode =
                System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.gridSource.ShowCellErrors = false;
            this.gridSource.Size = new System.Drawing.Size(519, 387);
            this.gridSource.TabIndex = 0;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(519, 147);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainSplitontainer);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(800, 600);
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

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.SplitContainer MainSplitontainer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid gridSettings;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.ToolStripMenuItem PingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KktStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem FiscalAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.DataGridView gridSource;
    }
}
