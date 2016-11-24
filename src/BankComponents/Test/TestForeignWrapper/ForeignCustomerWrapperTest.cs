using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Components.Common;
using Components.Wrapper.Foreign;
using System.Runtime.InteropServices;

namespace TestForeignWrapper
{
    [TestClass]
    public class ForeignCustomerWrapperTest
    {

        /*[TestMethod]
        public void createCustomerTest()
        {
            //var data = new ForeignCustomer() { firstName = "Seppi", lastName = "Deppi", id = 0 };
            //var data = new ForeignCustomer() { firstName = "Jolly", lastName = "Nobst", id = 0 };
            var data = new ForeignCustomer() { firstName = "Hans", lastName = "Wurst", id = 0 };
            int retVal = 0;

            retVal = CustomerWrapper.CreateCustomer(data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 3);

        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            //var compareData = new ForeignCustomer() { firstName = "Seppi", lastName = "Deppi", id = 1 };
            var compareData = new ForeignCustomer() { firstName = "Jolly", lastName = "Nobst", id = 2 };
            //var compareData = new ForeignCustomer() { firstName = "Hans", lastName = "Wurst", id = 1 };

            ForeignCustomerOut inputData = new ForeignCustomerOut();// { id = 0, firstName = new IntPtr(Marshal.SizeOf(typeof(string))), lastName = new IntPtr(Marshal.SizeOf(typeof(string)))};
            
            int id = 2;

            var retVal = CustomerWrapper.GetCustomerById(id, inputData);

            ForeignCustomer data = new ForeignCustomer(){ firstName = Marshal.PtrToStringAnsi(inputData.firstName), lastName = Marshal.PtrToStringAnsi(inputData.lastName), id = inputData.id};

            Assert.IsTrue(retVal == 1);
            Assert.AreEqual(compareData.id, data.id);
            Assert.AreEqual(compareData.firstName, data.firstName);
            Assert.AreEqual(compareData.lastName, data.lastName);
        }

        [TestMethod]
        public void updateCustomerTest()
        {
            //TODO: fix this! returns -6 (customer does not exist) from the data access layer though he exists
            var data = new ForeignCustomer() { firstName = "Foo", lastName = "Bar", id = 2 };
            int id = 2;
            int retVal = 0;

            retVal = CustomerWrapper.updateCustomer(id, data);

            Assert.IsTrue(retVal == 1);
            //Assert.IsTrue(data.id == 2);
        }

        [TestMethod]
        public void deleteCustomerTest()
        {
            int id = 2;
            var retVal = CustomerWrapper.DeleteCustomer(id);
            //TODO: fix this! causes a vector subscript out of range error
            // dll blows up in the data access layer method getByCustomerId(), 
            // which is used in this context to identify open dependencies to existing accounts.
            Assert.IsTrue(retVal == 1);
        }*/
    }
}
