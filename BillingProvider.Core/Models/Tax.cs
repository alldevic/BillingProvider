using System.ComponentModel;

namespace BillingProvider.Core.Models
{
    /// <summary>
    /// НДС в процентах или ТЕГ НДС. Теги 1102, 1103, 1104, 1105, 1106, 1107
    /// </summary>
    public enum Tax
    {
        /// <summary>
        /// -1 (НДС не облагается). Тег 1105
        /// </summary>
        [Description("НДС не облагается")] NoTax_1105 = -1,

        /// <summary>
        /// 0 (НДС 0%). Тег 1104 
        /// </summary>
        [Description("НДС 0%")] Tax0_1104 = 0,

        /// <summary>
        /// 10 (НДС 10%). Тег 1103
        /// </summary>
        [Description("НДС 10%")] Tax10_1103 = 10,

        /// <summary>
        /// 20 (НДС 20%). Тег 1102
        /// </summary>
        [Description("НДС 20%")] Tax20_1102 = 20,

        /// <summary>
        /// 110 (НДС 10 /110). Тег 1107
        /// </summary>
        [Description("НДС 10/110")] Tax110_1107 = 110,

        /// <summary>
        /// 120 (НДС 20 /120). Тег 1106
        /// </summary>
        [Description("НДС 20/120")] Tax120_1106 = 120
    }
}