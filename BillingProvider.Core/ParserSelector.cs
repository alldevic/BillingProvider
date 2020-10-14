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

            if (firstLine.Split(';').Length == 21 ||
                firstLine.Split(';').Length == 18)
            {
                Log.Debug("Select specsber parser");
                return new SpecSberParser(path);
            }
            
            if (firstLine.Split(';').Length == 12)
            {
                Log.Debug("Select specsberthree parser");
                return new SpecSberParserThree(path);
            }

            if (firstLine.Split('|').Length == 7)
            {
                Log.Debug("Select espsber parser");
                return new EspSberParser(path);
            }

            if (firstLine.Length < 10)
            {
                Log.Debug("Select CsvSberParser");
                return new CsvSberParser(path);
            }

            throw new ArgumentException();
        }
    }
}