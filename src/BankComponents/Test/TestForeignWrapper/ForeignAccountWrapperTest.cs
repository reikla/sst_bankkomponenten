using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Components.Common;
using Components.Wrapper.Foreign;

namespace TestForeignWrapper
{
    [TestClass]
    public class ForeignAccountWrapperTest
    {
        [TestMethod]
        public void CreateAccountTest()
        {
            //var data = new ForeignAccount() {iban = "AT1234567891011", bic = "AT1234567", customerId = 1, type= ForeignAccountType.GiroAccount, isActive=true };
            var data = new ForeignAccount() { iban = "XY1234567891011", bic = "XY1234567", customerId = 2, type = ForeignAccountType.SavingsAccount, isActive = true };

            int retVal = 0;

            retVal = AccountWrapper.createAccount(data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 1);
        }

        /*[TestMethod]
        public void GetAccountTest()
        {
            //TODO: fix this!
            var compareData = new ForeignAccountStruct() { iban = "AT1234567891011", bic = "AT1234567", customerId = 1, type = ForeignAccountType.GiroAccount, isActive = true, id = 1 };
            //compareData.id = 1;
            //compareData.firstName.Append("Hans");
            //compareData.lastName.Append("Wurst");
            ForeignAccountStruct inputData;// = new ForeignAccount();
            int id = 1;

            var retVal = AccountWrapper.getAccountById(id, out inputData);

            Assert.IsTrue(retVal == 1);
            Assert.AreEqual(compareData, inputData);
        }*/

        [TestMethod]
        public void updateAccountTest()
        {
            //TODO: fix this! does not persist changes although it commits a success
            var data = new ForeignAccount() { iban = "BLUB34567891011", bic = "ATX234567", customerId = 1, type = ForeignAccountType.GiroAccount, isActive = true , id=1};
            int id = 1;
            int retVal = 0;

            retVal = AccountWrapper.updateAccount(id,data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 1);
        }

        [TestMethod]
        public void closeAccountTest()
        {
            int id = 1;
            var retVal = AccountWrapper.closeAccount(id);

            Assert.IsTrue(retVal == 1);
        }

        [TestMethod]
        public void addDisposerTest()
        {
            //TODO: Fix this! the additional accessor data is not saved anywhere
            var data = new ForeignAccount() { iban = "AT1234567891011", bic = "AT1234567", customerId = 1, type = ForeignAccountType.GiroAccount, isActive = true };
            int id = 2;
            var retVal = AccountWrapper.addAccessor(id, data);

            Assert.IsTrue(retVal == 1);
        }

        [TestMethod]
        void removeDisposerTest()
        {
            //There is no such method to get infos about the available accessors
        }
    }
}
