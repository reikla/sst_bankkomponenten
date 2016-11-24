using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Components.Common;
using Components.Wrapper.Foreign;
using System.Runtime.InteropServices;

namespace TestForeignWrapper
{
    [TestClass]
    public class ForeignAccountWrapperTest
    {
        /*[TestMethod]
        public void CreateAccountTest()
        {
            var data = new ForeignAccount() {iban = "AT1234567891011", bic = "AT1234567", customerId = 1, type= ForeignAccountType.GiroAccount, isActive=true };
            //var data = new ForeignAccount() { iban = "XY1234567891011", bic = "XY1234567", customerId = 2, type = ForeignAccountType.SavingsAccount, isActive = true };

            int retVal = 0;

            retVal = AccountWrapper.createAccount(data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 2);
        }

        [TestMethod]
        public void GetAccountTest()
        {
            var compareData = new ForeignAccount() { id=1, iban = "XY1234567891011", bic = "XY1234567", customerId = 2, type = ForeignAccountType.SavingsAccount, isActive = true };
            
            ForeignAccountOut inputData = new ForeignAccountOut();
            int id = 1;

            var retVal = AccountWrapper.getAccountById(id, inputData);

            ForeignAccount data = new ForeignAccount() { id=inputData.id, customerId=inputData.customerId, bic= Marshal.PtrToStringAnsi(inputData.bic), iban=Marshal.PtrToStringAnsi(inputData.iban), isActive = inputData.isActive==1?true:false, type= inputData.type};
            Assert.IsTrue(retVal == 1);
            Assert.AreEqual(compareData.id, data.id);
            Assert.AreEqual(compareData.customerId, data.customerId);
            Assert.AreEqual(compareData.bic, data.bic);
            Assert.AreEqual(compareData.iban, data.iban);
            Assert.AreEqual(compareData.isActive, data.isActive);
            Assert.AreEqual(compareData.type, data.type);
        }

        [TestMethod]
        public void updateAccountTest()
        {
            //the id of the account is also be changed and if the given id does not exist it just creates a new account
            var data = new ForeignAccount() { iban = "BLAB34567891011", bic = "ATX234567", customerId = 2, type = ForeignAccountType.GiroAccount, isActive = false , id=1};
            int id = 2;
            int retVal = 0;

            retVal = AccountWrapper.updateAccount(id,data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 5);
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
            //There is no such method in the foreign-dll to get infos about the available accessors
        }*/
    }
}
