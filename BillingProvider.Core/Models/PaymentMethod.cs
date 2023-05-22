using System.ComponentModel;

namespace BillingProvider.Core.Models
{
    /// <summary>
    ///  Способ оплаты. Теги 1031, 1081, 1215, 1216, 1217
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Наличная оплата (2 знака после запятой), Тег 1031 
        /// </summary>
        [Description("Наличная оплата")] Cash_1031,

        /// <summary>
        /// Сумма электронной оплаты (2 знака после запятой), Тег 1081 
        /// </summary>
        [Description("Безналичная оплата")] ElectronicPayment_1081,

        /// <summary>
        /// Сумма из предоплаты (зачетом аванса) (2 знака после запятой), Тег 1215 
        /// </summary>
        [Description("Предоплата (с зачетом аванса)")]
        AdvancePayment_1215,

        /// <summary>
        /// Сумма постоплатой (в кредит) (2 знака после запятой), Тег 1216 
        /// </summary>
        [Description("Постоплата (в кредит)")] Credit_1216,

        /// <summary>
        /// Сумма оплаты встречным предоставлением (сертификаты, др. мат.ценности) (2 знака после запятой), Тег 1217 
        /// </summary>
        [Description("Оплата встречным представленем")]
        CashProvision_1217
    }
}