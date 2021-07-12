﻿using System.ComponentModel;

namespace BillingProvider.Core.Models
{
    /// <summary>
    /// Признак способа расчета. Тег ОФД 1214. Для ФФД.1.05 и выше обязательное поле
    /// </summary>
    public enum SignMethodCalculation
    {
        /// <summary>
        /// 1: "ПРЕДОПЛАТА 100% (Полная предварительная оплата до момента передачи предмета расчета)"
        /// </summary>
        [Description("ПРЕДОПЛАТА 100%")] FULL_PREPAYMENT = 1,

        /// <summary>
        /// 2: "ПРЕДОПЛАТА (Частичная предварительная оплата до момента передачи предмета расчета)" 
        /// </summary>
        [Description("ПРЕДОПЛАТА")] PREPAYMENT,

        /// <summary>
        /// 3: "АВАНС" 
        /// </summary>
        [Description("АВАНС")] ADVANCE,

        /// <summary>
        /// 4: "ПОЛНЫЙ РАСЧЕТ (Полная оплата, в том числе с учетом аванса в момент передачи предмета расчета)" 
        /// </summary>
        [Description("ПОЛНЫЙ РАСЧЕТ")] FULL_PAYMENT,

        /// <summary>
        /// 5: "ЧАСТИЧНЫЙ РАСЧЕТ И КРЕДИТ (Частичная оплата предмета расчета в момент его передачи с последующей оплатой в кредит )" 
        /// </summary>
        [Description("ЧАСТИЧНЫЙ РАСЧЕТ И КРЕДИТ")]
        PARTIAL_PAYMENT,

        /// <summary>
        /// 6: "ПЕРЕДАЧА В КРЕДИТ (Передача предмета расчета без его оплаты в момент его передачи с последующей оплатой в кредит)" 
        /// </summary>
        [Description("ПЕРЕДАЧА В КРЕДИТ")] CREDIT,

        /// <summary>
        /// 7: "ОПЛАТА КРЕДИТА (Оплата предмета расчета после его передачи с оплатой в кредит )" 
        /// </summary>
        [Description("ОПЛАТА КРЕДИТА")] CREDIT_PAYMENT
    }
}