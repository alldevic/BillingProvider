using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BillingProvider.Core
{
    public static class Utils
    {
        public static string NormalizeJson(this string input)
        {
            return Regex.Replace(input, @"\u00a0", " ");
        }
    }
}