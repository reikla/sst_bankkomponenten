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
        /*[ClassInitialize]
        public static void init(TestContext context)
        {
            var path = GetType().Assembly.CodeBase;
        }*/

        [TestMethod]
        public void createCustomerTest()
        {
            var data = new ForeignCustomer() { firstName = "Seppi", lastName = "Deppi", id = 0 };
            int retVal = 0;

            retVal = CustomerWrapper.CreateCustomer(data);

            Assert.IsTrue(retVal == 1);
            Assert.IsTrue(data.id == 4);

        }

        /*[TestMethod]
        public void GetCustomerByIdTest()
        {
            //TODO: fix this!
            var compareData = new ForeignCustomer() { firstName = "Hans", lastName = "Wurst", id = 1 };
            //compareData.id = 1;
            //compareData.firstName.Append("Hans");
            //compareData.lastName.Append("Wurst");
            ForeignCustomer inputData = new ForeignCustomer();
            //IntPtr inputData = new IntPtr(Marshal.SizeOf(typeof(ForeignCustomerOut)));
            int id = 1;

            //var retVal = CustomerWrapper.GetGetCustomerById(id, out inputData);
            var retVal = CustomerWrapper.GetGetCustomerById(id, out inputData);

            //ForeignCustomer data = new ForeignCustomer(){ firstName = Marshal.PtrToStringAuto(inputData.firstName), lastName = Marshal.PtrToStringAuto(inputData.lastName), id = inputData.id};
            //ForeignCustomerOut data = Marshal.PtrToStructure<ForeignCustomerOut>(inputData);//new IntPtr(inputData.ToInt32() + Marshal.SizeOf(typeof(ForeignCustomer))));

            Assert.IsTrue(retVal == 1);
            Assert.AreEqual(compareData, inputData);
        }*/

//        [TestMethod]
//        public void updateCustomerTest()
//        {
//            //TODO: fix this!
//            var data = new ForeignCustomer() { firstName = "Jolly", lastName = "Nobst", id = 2 };
//            int id = 2;
//            int retVal = 0;
//
//            retVal = CustomerWrapper.updateCustomer(id, data);
//
//            Assert.IsTrue(retVal == 1);
//            Assert.IsTrue(data.id == 2);
//        }

        [TestMethod]
        public void deleteCustomerTest()
        {
            int id = 2;
            var retVal = CustomerWrapper.DeleteCustomer(id);
            //TODO: fix this! causes a vector subscript out of range error
            // dll blows up in the data access layer method getByCustomerId(), 
            // which is used in this context to identify open dependencies to existing accounts.
            Assert.IsTrue(retVal == 1);
        }
    }
}
