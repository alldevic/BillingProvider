using System;
using System.ComponentModel;
using System.Configuration;
using NLog;

namespace BillingProvider.KkmServer
{
    //TODO: validation

    public class AppSettings
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Server

        [Category("Сервер")]
        [Description("IP-адрес сервера с кассой")]
        [DisplayName("Адрес")]
        public string ServerAddress { get; set; } = "127.0.0.1";

        [Category("Сервер")]
        [Description("Порт, на котором запущен сервер")]
        [DisplayName("Порт")]
        public int ServerPort { get; set; } = 5893;

        [Category("Сервер")]
        [Description("Номер устройства на сервере, 0 - первое активное")]
        [DisplayName("Номер устройства")]
        public int ServerDeviceId { get; set; }


        [Category("Сервер")]
        [Description("Имя учетной записи kktserver")]
        [DisplayName("Логин")]
        public string ServerLogin { get; set; } = "Admin";

        [Category("Сервер")]
        [Description("Пароль от учетной записи kktserver")]
        [DisplayName("Пароль")]
        public string ServerPassword { get; set; } = "";

        #endregion

        #region Cashier

        [Category("Кассир")]
        [Description("Фамилия и инициалы текущего кассира")]
        [DisplayName("Имя")]
        [RefreshProperties(RefreshProperties.All)]
        public string CashierName { get; set; }

        [Category("Кассир")]
        [Description("ИНН текущего кассира")]
        [DisplayName("ИНН")]
        [RefreshProperties(RefreshProperties.All)]
        public string CashierVatin { get; set; }

        #endregion

        #region Company

        [Category("Компания")]
        [Description("Адрес электронной почты компании")]
        [DisplayName("Email")]
        [RefreshProperties(RefreshProperties.All)]
        public string CompanyMail { get; set; }

        #endregion

        public AppSettings()
        {
            Log.Debug("Begin app settings loading");
            try
            {
                ServerAddress = ConfigurationManager.AppSettings[$"{nameof(ServerAddress)}"];
                Log.Trace($"{nameof(ServerAddress)}='{ServerAddress}'");

                ServerPort = int.Parse(ConfigurationManager.AppSettings[$"{nameof(ServerPort)}"]);
                Log.Trace($"{nameof(ServerPort)}='{ServerPort}'");

                ServerDeviceId = int.Parse(ConfigurationManager.AppSettings[$"{nameof(ServerDeviceId)}"]);
                Log.Trace($"{nameof(ServerDeviceId)}='{ServerDeviceId}'");

                ServerLogin = ConfigurationManager.AppSettings[$"{nameof(ServerLogin)}"];
                Log.Trace($"{nameof(ServerLogin)}='{ServerLogin}'");

                ServerPassword = ConfigurationManager.AppSettings[$"{nameof(ServerPassword)}"];
                Log.Trace($"{nameof(ServerPassword)}='{ServerPassword}'");

                CashierName = ConfigurationManager.AppSettings[$"{nameof(CashierName)}"];
                Log.Trace($"{nameof(CashierName)}='{CashierName}'");

                CashierVatin = ConfigurationManager.AppSettings[$"{nameof(CashierVatin)}"];
                Log.Trace($"{nameof(CashierVatin)}='{CashierVatin}'");

                CompanyMail = ConfigurationManager.AppSettings[$"{nameof(CompanyMail)}"];
                Log.Trace($"{nameof(CompanyMail)}='{CompanyMail}'");

                Check();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Error settings loading");
            }
            finally
            {
                Log.Debug("End app settings loading");
                Log.Info("Настройки успешно загружены!");
            }
        }

        private void Check()
        {
            Log.Debug("Begin checking app settings");

            //TODO

            Log.Debug("End checking app settings");
        }

        public void UpdateSettings()
        {
            Log.Debug("Begin saving app settings");

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings[nameof(ServerAddress)].Value = ServerAddress;
            Log.Trace($"{nameof(ServerAddress)}='{ServerAddress}'");

            configuration.AppSettings.Settings[nameof(ServerPort)].Value = ServerPort.ToString();
            Log.Trace($"{nameof(ServerPort)}='{ServerPort}'");

            configuration.AppSettings.Settings[nameof(ServerLogin)].Value = ServerLogin;
            Log.Trace($"{nameof(ServerLogin)}='{ServerLogin}'");

            configuration.AppSettings.Settings[nameof(ServerPassword)].Value = ServerPassword;
            Log.Trace($"{nameof(ServerPassword)}='{ServerPassword}'");

            configuration.AppSettings.Settings[nameof(CashierName)].Value = CashierName;
            Log.Trace($"{nameof(CashierName)}='{CashierName}'");

            configuration.AppSettings.Settings[nameof(CashierVatin)].Value = CashierVatin;
            Log.Trace($"{nameof(CashierVatin)}='{CashierVatin}'");

            configuration.AppSettings.Settings[nameof(CompanyMail)].Value = CompanyMail;
            Log.Trace($"{nameof(CompanyMail)}='{CompanyMail}'");

            configuration.AppSettings.Settings[nameof(ServerDeviceId)].Value = ServerDeviceId.ToString();
            Log.Trace($"{nameof(ServerDeviceId)}='{ServerDeviceId}'");


            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
            Log.Debug("End saving app settings");
            Log.Info("Настройки сохранены!");
        }
    }
}