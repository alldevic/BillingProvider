using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using BillingProvider.Core.Models;
using BillingProvider.WinForms.Extensions;
using NLog;

namespace BillingProvider.WinForms
{
    public class AppSettings : FilterablePropertyBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region AtolOnline

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description("ИНН организации; сравнивается со значением в фискальном накопителе")]
        [DisplayName("ИНН")]
        public string AtolOnlineINN { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description("Код группы ККТ, относящейся к интернет-магазину, зарегистрированному в “АТОЛ Онлайн”")]
        [DisplayName("Идентификатор группы ККТ")]
        public string AtolOnlineGroupID { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description("При расчётах в интернете это адрес сайта; сравнивается со значением в фискальном накопителе")]
        [DisplayName("Адрес места расчетов")]
        public string AtolOnlineHostname { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description(
            "Логин пользователя для отправки данных. Его можно получить из файла настроек для CMS в личном кабинете пользователя “АТОЛ Онлайн”")]
        [DisplayName("Логин")]
        [RefreshProperties(RefreshProperties.All)]
        public string AtolOnlineLogin { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description(
            "Пароль пользователя для отправки данных. Его можно получить из файла настроек для CMS в личном кабинете пользователя “АТОЛ Онлайн”")]
        [DisplayName("Пароль")]
        [RefreshProperties(RefreshProperties.All)]
        public string AtolOnlinePassword { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description(
            "Адрес тестовой среды (ФФД 1.05): https://testonline.atol.ru/possystem/v4/" +
            "Адрес production: https://online.atol.ru/possystem/v4/")]
        [DisplayName("Адрес среды:")]
        [RefreshProperties(RefreshProperties.All)]
        public string AtolHost { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description(
            "Задержка в миллисекундах между обращения к АТОЛ Онлайн. Слишком маленькое число может привести к ошибкам, в связи с частыми обращением к серверу")]
        [DisplayName("Задержка (мс)")]
        public int AtolOnlineDelay { get; set; } = 2500;

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description("Текущий токен Atol Online")]
        [DisplayName("Токен")]
        [RefreshProperties(RefreshProperties.All)]
        [ReadOnly(true)]
        public string AtolToken { get; set; } = "";

        [CustomSortedCategory("АТОЛ Онлайн", 5, 6)]
        [Description(
            "Дата истечения токена")]
        [DisplayName("Дата истечения")]
        [RefreshProperties(RefreshProperties.All)]
        [ReadOnly(true)]
        public DateTime AtolTokenExpiredDateTime { get; set; }

        #endregion

        #region Cashier

        [CustomSortedCategory("Кассир", 1, 6)]
        [Description("Фамилия и инициалы текущего кассира")]
        [DisplayName("Имя")]
        [RefreshProperties(RefreshProperties.All)]
        public string CashierName { get; set; }

        [CustomSortedCategory("Кассир", 1, 6)]
        [Description("ИНН текущего кассира")]
        [DisplayName("ИНН")]
        [RefreshProperties(RefreshProperties.All)]
        public string CashierVatin { get; set; }

        #endregion

        #region Common

        [CustomSortedCategory("Общие", 3, 6)]
        [Description("Адрес электронной почты компании")]
        [DisplayName("Email")]
        [RefreshProperties(RefreshProperties.All)]
        public string CompanyMail { get; set; }


        [CustomSortedCategory("Общие", 3, 6)]
        [Description("Способ оплаты по умолчанию")]
        [DisplayName("Способ оплаты")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(EnumTypeConverter))]
        public PaymentMethod ServerPaymentMethod { get; set; } = PaymentMethod.Cash_1031;

        [CustomSortedCategory("Общие", 3, 6)]
        [Description("Признак способа расчета по умолчанию")]
        [DisplayName("Признак способа расчета")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(EnumTypeConverter))]
        public SignMethodCalculation ServerSignMethodCalculation { get; set; } = SignMethodCalculation.FULL_PAYMENT;

        #endregion

        #region Watcher

        [CustomSortedCategory("Отслеживание папки", 6, 6)]
        [Description("Путь до папки")]
        [DisplayName("Папка")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        public string FolderPath { get; set; }

        [CustomSortedCategory("Отслеживание папки", 6, 6)]
        [Description("Сканирование подпапок на наличие изменений")]
        [DisplayName("Включая подпапки")]
        [TypeConverter(typeof(BooleanToYesNoTypeConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public bool IncludeSubfolders { get; set; }

        #endregion

        #region Defaults

        [CustomSortedCategory("Умолчания", 2, 6)]
        [Description("Наименование позиции по умолчанию")]
        [DisplayName("Позиция")]
        [RefreshProperties(RefreshProperties.All)]
        public string DefaultPositionName { get; set; }

        [CustomSortedCategory("Умолчания", 2, 6)]
        [Description("Использовать имя позиции по умолчанию")]
        [DisplayName("Использовать позицию")]
        [TypeConverter(typeof(BooleanToYesNoTypeConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public bool PositionEnabled { get; set; }

        [CustomSortedCategory("Умолчания", 2, 6)]
        [Description("Подставлять сумму в позицию сгенерированную по умолчанию (только таблица)")]
        [DisplayName("Подставлять сумму")]
        [TypeConverter(typeof(BooleanToYesNoTypeConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public bool AutosumEnabled { get; set; }

        [CustomSortedCategory("Умолчания", 2, 6)]
        [Description("Обновлять позицию даже если в ней уже записано значени")]
        [DisplayName("Пересчитывать принудительно")]
        [TypeConverter(typeof(BooleanToYesNoTypeConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public bool ForceAutosumEnabled { get; set; }

        #endregion


        public AppSettings()
        {
            Log.Debug("Begin app settings loading");
            try
            {
                AtolOnlineINN = ConfigurationManager.AppSettings[$"{nameof(AtolOnlineINN)}"];
                Log.Trace($"{nameof(AtolOnlineINN)}='{AtolOnlineINN}'");

                AtolOnlineGroupID = ConfigurationManager.AppSettings[$"{nameof(AtolOnlineGroupID)}"];
                Log.Trace($"{nameof(AtolOnlineGroupID)}='{AtolOnlineGroupID}'");

                AtolOnlineHostname = ConfigurationManager.AppSettings[$"{nameof(AtolOnlineHostname)}"];
                Log.Trace($"{nameof(AtolOnlineHostname)}='{AtolOnlineHostname}'");

                AtolOnlineLogin = ConfigurationManager.AppSettings[$"{nameof(AtolOnlineLogin)}"];
                Log.Trace($"{nameof(AtolOnlineLogin)}='{AtolOnlineLogin}'");

                AtolOnlinePassword = ConfigurationManager.AppSettings[$"{nameof(AtolOnlinePassword)}"];
                Log.Trace($"{nameof(AtolOnlinePassword)}='{AtolOnlinePassword}'");

                AtolOnlineDelay = int.Parse(ConfigurationManager.AppSettings[$"{nameof(AtolOnlineDelay)}"]);
                Log.Trace($"{nameof(AtolOnlineDelay)}='{AtolOnlineDelay}'");

                CashierName = ConfigurationManager.AppSettings[$"{nameof(CashierName)}"];
                Log.Trace($"{nameof(CashierName)}='{CashierName}'");

                CashierVatin = ConfigurationManager.AppSettings[$"{nameof(CashierVatin)}"];
                Log.Trace($"{nameof(CashierVatin)}='{CashierVatin}'");

                CompanyMail = ConfigurationManager.AppSettings[$"{nameof(CompanyMail)}"];
                Log.Trace($"{nameof(CompanyMail)}='{CompanyMail}'");

                FolderPath = ConfigurationManager.AppSettings[$"{nameof(FolderPath)}"];
                Log.Trace($"{nameof(FolderPath)}='{FolderPath}'");

                IncludeSubfolders = Convert.ToBoolean(ConfigurationManager.AppSettings[$"{nameof(IncludeSubfolders)}"]);
                Log.Trace($"{nameof(IncludeSubfolders)}='{IncludeSubfolders}'");

                AtolHost = ConfigurationManager.AppSettings[$"{nameof(AtolHost)}"];
                Log.Trace($"{nameof(AtolHost)}='{AtolHost}'");

                DefaultPositionName = ConfigurationManager.AppSettings[$"{nameof(DefaultPositionName)}"];
                Log.Trace($"{nameof(DefaultPositionName)}='{DefaultPositionName}'");

                PositionEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[$"{nameof(PositionEnabled)}"]);
                Log.Trace($"{nameof(PositionEnabled)}='{PositionEnabled}'");

                AutosumEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[$"{nameof(AutosumEnabled)}"]);
                Log.Trace($"{nameof(AutosumEnabled)}='{AutosumEnabled}'");

                ForceAutosumEnabled =
                    Convert.ToBoolean(ConfigurationManager.AppSettings[$"{nameof(ForceAutosumEnabled)}"]);
                Log.Trace($"{nameof(ForceAutosumEnabled)}='{ForceAutosumEnabled}'");

                ServerPaymentMethod =
                    (PaymentMethod) Convert.ToInt32(ConfigurationManager.AppSettings[$"{nameof(ServerPaymentMethod)}"]);
                Log.Trace($"{nameof(ServerPaymentMethod)}='{ServerPaymentMethod}'");

                ServerSignMethodCalculation =
                    (SignMethodCalculation) Convert.ToInt32(
                        ConfigurationManager.AppSettings[$"{nameof(ServerSignMethodCalculation)}"]);
                Log.Trace($"{nameof(ServerSignMethodCalculation)}='{ServerSignMethodCalculation}'");
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

        public void UpdateSettings()
        {
            Log.Debug("Begin saving app settings");

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings[nameof(AtolOnlineINN)].Value = AtolOnlineINN;
            Log.Trace($"{nameof(AtolOnlineINN)}='{AtolOnlineINN}'");

            configuration.AppSettings.Settings[nameof(AtolOnlineGroupID)].Value = AtolOnlineGroupID;
            Log.Trace($"{nameof(AtolOnlineGroupID)}='{AtolOnlineGroupID}'");

            configuration.AppSettings.Settings[nameof(AtolOnlineHostname)].Value = AtolOnlineHostname;
            Log.Trace($"{nameof(AtolOnlineHostname)}='{AtolOnlineHostname}'");

            configuration.AppSettings.Settings[nameof(AtolOnlineLogin)].Value = AtolOnlineLogin;
            Log.Trace($"{nameof(AtolOnlineLogin)}='{AtolOnlineLogin}'");

            configuration.AppSettings.Settings[nameof(AtolOnlinePassword)].Value = AtolOnlinePassword;
            Log.Trace($"{nameof(AtolOnlinePassword)}='{AtolOnlinePassword}'");

            configuration.AppSettings.Settings[nameof(AtolOnlineDelay)].Value = AtolOnlineDelay.ToString();
            Log.Trace($"{nameof(AtolOnlineDelay)}='{AtolOnlineDelay}'");

            configuration.AppSettings.Settings[nameof(CashierName)].Value = CashierName;
            Log.Trace($"{nameof(CashierName)}='{CashierName}'");

            configuration.AppSettings.Settings[nameof(CashierVatin)].Value = CashierVatin;
            Log.Trace($"{nameof(CashierVatin)}='{CashierVatin}'");

            configuration.AppSettings.Settings[nameof(CompanyMail)].Value = CompanyMail;
            Log.Trace($"{nameof(CompanyMail)}='{CompanyMail}'");


            configuration.AppSettings.Settings[nameof(FolderPath)].Value = FolderPath;
            Log.Trace($"{nameof(FolderPath)}='{FolderPath}'");

            configuration.AppSettings.Settings[nameof(IncludeSubfolders)].Value = IncludeSubfolders.ToString();
            Log.Trace($"{nameof(IncludeSubfolders)}='{IncludeSubfolders}'");

            configuration.AppSettings.Settings[nameof(AtolHost)].Value = AtolHost;
            Log.Trace($"{nameof(AtolHost)}='{AtolHost}'");

            configuration.AppSettings.Settings[nameof(DefaultPositionName)].Value = DefaultPositionName;
            Log.Trace($"{nameof(DefaultPositionName)}='{DefaultPositionName}'");

            configuration.AppSettings.Settings[nameof(PositionEnabled)].Value = PositionEnabled.ToString();
            Log.Trace($"{nameof(PositionEnabled)}='{PositionEnabled}'");

            configuration.AppSettings.Settings[nameof(AutosumEnabled)].Value = AutosumEnabled.ToString();
            Log.Trace($"{nameof(AutosumEnabled)}='{AutosumEnabled}'");

            configuration.AppSettings.Settings[nameof(ForceAutosumEnabled)].Value = ForceAutosumEnabled.ToString();
            Log.Trace($"{nameof(ForceAutosumEnabled)}='{ForceAutosumEnabled}'");

            configuration.AppSettings.Settings[nameof(ServerPaymentMethod)].Value =
                ((int) ServerPaymentMethod).ToString();
            Log.Trace($"{nameof(ServerPaymentMethod)}='{ServerPaymentMethod}'");

            configuration.AppSettings.Settings[nameof(ServerSignMethodCalculation)].Value =
                ((int) ServerSignMethodCalculation).ToString();
            Log.Trace($"{nameof(ServerSignMethodCalculation)}='{ServerSignMethodCalculation}'");

            configuration.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
            Log.Debug("End saving app settings");
            Log.Info("Настройки сохранены!");
        }
    }
}