using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace BillingProvider.WinForms.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumerationValue)
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($@"{nameof(enumerationValue)} must be of Enum type",
                    nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute) attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
        
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }
    }
}