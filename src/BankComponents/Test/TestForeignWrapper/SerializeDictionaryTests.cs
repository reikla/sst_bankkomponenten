using System;
using System.Collections.Generic;
using System.Linq;
using BankMessage;
using ForeignBankComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForeignWrapper
{
    [TestClass]
    public class SerializeDictionaryTests
    {
        [TestMethod]
        public void SerializeMessages_Roundtrip_OK()
        {
            var dict = new Dictionary<long, Message>
            {
                {
                    1,
                    new Message(1, 1.2, DateTimeOffset.Now.AddHours(8), "reimar_klammer@gmx.at", "reimar_klammer@gmx.at",
                        "EUR", "1", "2", TransactionType.ACK, "Ueberweisung", 1)
                },
                {
                    2,
                    new Message(1, 1.2, DateTimeOffset.Now.AddHours(8), "reimar_klammer@gmail.com",
                        "reimar_klammer@gmail.com",
                        "EUR", "1", "2", TransactionType.ACK, "Ueberweisung", 2)
                }
            };


            var serializer = new SentMessageSerializer();
            serializer.Store(dict);

            var dict2 = serializer.Load();

            Assert.AreEqual(2, dict2.Count);

            Assert.AreEqual(dict2[dict.Keys.First()].Ablaufdatum, dict.Values.First().Ablaufdatum);
        }
    }
}
