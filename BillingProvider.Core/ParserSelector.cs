using System;
using System.IO;
using System.Linq;
using BillingProvider.Core.Parsers;
using NLog;

namespace BillingProvider.Core
{
    public static class ParserSelector
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static IParser Select(string path)
        {
            var firstLine = File.ReadLines(path).First();
            
            if (string.Equals(firstLine, "[") || firstLine.StartsWith("{"))
            {
                Log.Debug("Select inner parser");
                return new InnerParser(path);
            }

            if (firstLine.Contains("html"))
            {
                Log.Debug("Select HtmlKbbParser");
                return new HtmlKbbParser(path);
            }

            if (path.Contains("Реестр ЭТ"))
            {
                Log.Debug("Select etxlsx parser");
                return new EtXlsxParser(path);
            }

            if (path.EndsWith("xlsx") || path.EndsWith("xls"))
            {
                Log.Debug("Select xlsx parser");
                return new XlsxParser(path);
            }

            if (firstLine == "1CClientBankExchange")
            {
                Log.Debug("Select OneCParser");
                return new OneCParser(path);
            }

            if (firstLine.Contains("#"))
            {
                Log.Debug("Select CsvKbbPareer");
                return new CsvKbbParser(path);
            }

            if (firstLine.Split(';').Length == 5)
            {
                Log.Debug("Select txtmail parser");
                return new TxtMailParser(path);
            }

            if (firstLine.Split(new[]{"[!]"}, StringSplitOptions.None).Length == 3)
            {
                Log.Debug("Select specsber parser");
                return new SpecSberParser(path);
            }

            if (firstLine.Split('|').Length == 7)
            {
                Log.Debug("Select espsber parser");
                return new EspSberParser(path);
            }


            throw new ArgumentException();
        }
    }
}