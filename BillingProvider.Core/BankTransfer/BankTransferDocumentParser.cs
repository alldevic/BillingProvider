using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NLog;

namespace BillingProvider.Core.BankTransfer
{
    public class BankTransferDocumentParser
    {
        public string DocumentPath { get; private set; }

        public List<TransferDocument> TransferDocuments;
        public Dictionary<string, string> DocumentProperties;
        public List<Dictionary<string, string>> Accounts;

        private List<Dictionary<string, string>> documents;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public BankTransferDocumentParser(string documentPath)
        {
            DocumentProperties = new Dictionary<string, string>();
            Accounts = new List<Dictionary<string, string>>();
            documents = new List<Dictionary<string, string>>();
            DocumentPath = documentPath;
        }

        public void Parse()
        {
            int i;
            //TODO Сделать выбор кодировки. Возможна кодировка DOS 866.
            using (var reader = new StreamReader(DocumentPath))
            {
                //Проверяем заголовок документа
                if (reader.ReadLine() != "1CClientBankExchange")
                    return;

                //Читаем тело документа
                while (reader.Peek() >= 0)
                {
                    string data = reader.ReadLine();

                    //Читаем свойства документа
                    while (!data.StartsWith("СекцияРасчСчет"))
                    {
                        if (!String.IsNullOrWhiteSpace(data))
                        {
                            var dataArray = data.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                            if (dataArray.Length == 2)
                            {
                                i = 1;
                                if (DocumentProperties.ContainsKey(dataArray[0]))
                                {
                                    while (DocumentProperties.ContainsKey(dataArray[0] + i))
                                    {
                                        i++;
                                    }

                                    dataArray[0] += i;
                                    i = 1;
                                }

                                DocumentProperties.Add(dataArray[0], dataArray[1]);
                            }
                        }

                        data = reader.ReadLine();
                    }

                    //Читаем рассчетные счета
                    i = -1;
                    while (!data.StartsWith("СекцияДокумент"))
                    {
                        if (!String.IsNullOrWhiteSpace(data))
                        {
                            if (data.StartsWith("СекцияРасчСчет"))
                                i++;
                            if (Accounts.Count <= i)
                                Accounts.Add(new Dictionary<string, string>());
                            var dataArray = data.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                            if (dataArray.Length == 2)
                            {
                                Accounts[i].Add(dataArray[0], dataArray[1]);
                            }
                        }

                        data = reader.ReadLine();
                    }

                    //Читаем документы
                    i = -1;
                    while (!data.StartsWith("КонецФайла"))
                    {
                        if (!String.IsNullOrWhiteSpace(data))
                        {
                            if (data.StartsWith("СекцияДокумент"))
                                i++;
                            if (documents.Count <= i)
                                documents.Add(new Dictionary<string, string>());
                            var dataArray = data.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (dataArray.Length == 2)
                            {
                                documents[i].Add(dataArray[0], dataArray[1]);
                            }
                            else if (dataArray.Length == 1)
                            {
                                documents[i].Add(dataArray[0], String.Empty);
                            }
                        }

                        data = reader.ReadLine();
                    }
                }
            }

            //Создаем список на основе доменной модели документа.
            FillData();
        }

        private void FillData()
        {
            //Для разбора дат и сумм.
            var culture = CultureInfo.CreateSpecificCulture("ru-RU");
            culture.NumberFormat.NumberDecimalSeparator = ".";

            TransferDocuments = new List<TransferDocument>();
            foreach (var document in documents)
            {
                if (document["СекцияДокумент"] == "Мемориальный ордер")
                {
                    logger.Info("Пропущена секция \"Мемориальный ордер\"...");
                    continue;
                }

                TransferDocument doc = new TransferDocument();
                doc.DocumentType = TransferDocument.GetDocTypeFromString(document["СекцияДокумент"]);
                doc.Number = document["Номер"];
                doc.Date = DateTime.Parse(document["Дата"], culture);
                doc.Total = Decimal.Parse(document["Сумма"], culture.NumberFormat);
                doc.PayerAccount = document["ПлательщикСчет"];
                if (document.ContainsKey("ДатаСписано") && !String.IsNullOrWhiteSpace(document["ДатаСписано"]))
                    doc.WriteoffDate = DateTime.Parse(document["ДатаСписано"], culture);
               
                
                if (document.ContainsKey("Плательщик"))
                    doc.PayerName = document["Плательщик"];
                else
                    doc.PayerName = document["Плательщик1"];
//                if (doc.PayerName.Contains("р/с") &&
//                    !String.IsNullOrWhiteSpace(doc.PayerName.Substring(0, doc.PayerName.IndexOf("р/с"))))
//                    doc.PayerName = doc.PayerName.Substring(0, doc.PayerName.IndexOf("р/с"));
//                if (doc.PayerName.Contains("//") &&
//                    !String.IsNullOrWhiteSpace(doc.PayerName.Substring(0, doc.PayerName.IndexOf("//"))))
//                    doc.PayerName = doc.PayerName.Substring(0, doc.PayerName.IndexOf("//"));

                
                
                doc.PayerInn = document["ПлательщикИНН"];
                doc.PayerKpp = document["ПлательщикКПП"];
                doc.PayerCheckingAccount = document["ПлательщикРасчСчет"];
                doc.PayerBank = document["ПлательщикБанк1"];
                doc.PayerBik = document["ПлательщикБИК"];
                doc.PayerCorrespondentAccount = document["ПлательщикКорсчет"];
                doc.RecipientAccount = document["ПолучательСчет"];
                if (document.ContainsKey("ДатаПоступило") && !String.IsNullOrWhiteSpace(document["ДатаПоступило"]))
                    doc.ReceiptDate = DateTime.Parse(document["ДатаПоступило"], culture);
                if (document.ContainsKey("Получатель"))
                    doc.RecipientName = document["Получатель"];
                else
                    doc.RecipientName = document["Получатель1"];
                if (doc.RecipientName.Contains("р/с") &&
                    !String.IsNullOrWhiteSpace(doc.RecipientName.Substring(0, doc.RecipientName.IndexOf("р/с", StringComparison.Ordinal))))
                    doc.RecipientName = doc.RecipientName.Substring(0, doc.RecipientName.IndexOf("р/с", StringComparison.Ordinal));
                if (doc.RecipientName.Contains("//") &&
                    !String.IsNullOrWhiteSpace(doc.RecipientName.Substring(0, doc.RecipientName.IndexOf("//", StringComparison.Ordinal))))
                    doc.RecipientName = doc.RecipientName.Substring(0, doc.RecipientName.IndexOf("//", StringComparison.Ordinal));
                doc.RecipientInn = document["ПолучательИНН"];
                doc.RecipientKpp = document["ПолучательКПП"];
                doc.RecipientCheckingAccount = document["ПолучательРасчСчет"];
                doc.RecipientBank = document["ПолучательБанк1"];
                doc.RecipientBik = document["ПолучательБИК"];
                doc.RecipientCorrespondentAccount = document["ПолучательКорсчет"];
                doc.PaymentPurpose = document["НазначениеПлатежа"];

//                string value;
//                value = StringWorks.Replaces.Values.FirstOrDefault(doc.PayerName.Contains);
//                if (!String.IsNullOrWhiteSpace(value))
//                {
//                    doc.PayerName = doc.PayerName.Replace(value,
//                        StringWorks.Replaces.Keys.FirstOrDefault(key => StringWorks.Replaces[key] == value));
//                }
//
//                value = StringWorks.Replaces.Values.FirstOrDefault(doc.RecipientName.Contains);
//                if (!String.IsNullOrWhiteSpace(value))
//                {
//                    doc.RecipientName = doc.RecipientName.Replace(value,
//                        StringWorks.Replaces.Keys.FirstOrDefault(key => StringWorks.Replaces[key] == value));
//                }

                TransferDocuments.Add(doc);
            }
        }
    }
}