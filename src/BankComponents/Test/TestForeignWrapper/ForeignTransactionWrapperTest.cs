using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Components.Wrapper.Foreign;
using Components.Common;

namespace TestForeignWrapper
{
    [TestClass]
    public class ForeignTransactionWrapperTest
    {
        [TestMethod]
        public void payOutTest()
        {
            ForeignCurrency currency = ForeignCurrency.EUR;
            Double amount = 125.35;
            int accountId = 1;
            var retVal = TransactionWrapper.auszahlung(currency, amount, accountId);

            Assert.IsTrue(retVal == 1);

        }

        [TestMethod]
        public void payInTest()
        {
            //ForeignCurrency currency = ForeignCurrency.EUR;
            //Double amount = 200;
            //int accountId = 1;

            ForeignCurrency currency = ForeignCurrency.JPY;
            Double amount = 100;
            int accountId = 2;
            var retVal = TransactionWrapper.einzahlung(currency, amount, accountId);

            Assert.IsTrue(retVal == 1);
        }

        [TestMethod]
        public void transferTest()
        {
            ForeignCurrency currency = ForeignCurrency.EUR;
            Double amount = 10.35;
            int targetId = 1;
            int sourceId = 2;
            var retVal = TransactionWrapper.ueberweisung(currency, amount, targetId, sourceId);

            Assert.IsTrue(retVal == 1);
        }

        [TestMethod]
        public void accountStatementTest()
        {
            //There is no possibility to get all transactions with the foreign dlls
        }

        [TestMethod]
        public void accountBalancingTest()
        {
            int id = 1;
            ForeignCurrency currency = ForeignCurrency.EUR;
            Double saldo = 0;
            var retVal = TransactionWrapper.calcSaldo(id, currency, out saldo);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(Math.Abs(74.65-saldo)<0.1);

        }
    }
}
