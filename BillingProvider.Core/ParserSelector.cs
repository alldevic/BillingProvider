using System;
using System.IO;
using System.Linq;
using BillingProvider.Core.Models;
using BillingProvider.Core.Parsers;
using NLog;

namespace BillingProvider.Core
{
    public static class ParserSelector
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static IParser Select(string path, PaymentMethod paymentMethod,
            SignMethodCalculation signMethodCalculation)
        {
            var firstLine = File.ReadLines(path).First();

            if (path.EndsWith(".txt") && firstLine.Split(';').Length == 6)
            {
                Log.Debug("Select Tinkoff parser");
                return new TinParser(path, paymentMethod, signMethodCalculation);    
            }

            if (string.Equals(firstLine, "[") || firstLine.StartsWith("{"))
            {
                Log.Debug("Select inner parser");
                return new InnerParser(path, paymentMethod, signMethodCalculation);
            }

            if (firstLine.Contains("html"))
            {
                Log.Debug("Select HtmlKbbParser");
                return new HtmlKbbParser(path, paymentMethod, signMethodCalculation);
            }

            if (path.Contains("Реестр ЭТ"))
            {
                Log.Debug("Select etxlsx parser");
                return new EtXlsxParser(path, paymentMethod, signMethodCalculation);
            }

            if (path.EndsWith("xlsx") || path.EndsWith("xls"))
            {
                Log.Debug("Select xlsx parser");
                return new XlsxParser(path, paymentMethod, signMethodCalculation);
            }

            if (firstLine == "1CClientBankExchange")
            {
                Log.Debug("Select OneCParser");
                return new OneCParser(path, paymentMethod, signMethodCalculation);
            }

            if ((firstLine.Split('#').Length == 2) && (firstLine.Split(';').Length == 2))
            {
                Log.Debug("Select CsvKbbParser");
                return new CsvKbbParser(path, paymentMethod, signMethodCalculation);
            }

            if (firstLine.Split(';').Length == 5)
            {
                Log.Debug("Select txtmail parser");
                return new TxtMailParser(path, paymentMethod, signMethodCalculation);
            }

            if (firstLine.Split(new[] {"[!]"}, StringSplitOptions.None).Length == 3)
            {
                Log.Debug("Select specsber parser");
                return new SpecSberParser(path, paymentMethod, signMethodCalculation);
            }

            if (firstLine.Split('|').Length == 7)
            {
                Log.Debug("Select espsber parser");
                return new EspSberParser(path, paymentMethod, signMethodCalculation);
            }


            throw new ArgumentException();
        }
    }
}