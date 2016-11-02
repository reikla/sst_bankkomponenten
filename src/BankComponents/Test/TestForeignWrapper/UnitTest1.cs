using Components.Common;
using Components.Wrapper.Foreign;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForeignWrapper
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            for (int i = 0; i < 10; i++)
            {
                
            var data = new ForeignCustomerStruct() {firstName = "Hans",lastName = "Wurst"};

            var  path = GetType().Assembly.CodeBase;

            int v1, v2;


            var isConnected = PersistenceWrapper.ConnectDataAccessLayer();

            //var retVal = CustomerWrapper.CreateCustomer(ref data);


              //  Assert.IsTrue(1 == retVal);


            //var ptr = PersistenceWrapper.SelectCustomerById(data.id, out v1);

            //var d = new ForeignAccountStruct[v1];
               


            //var d =         (ForeignCustomerStruct)    Marshal.PtrToStructure(ptr, typeof(ForeignCustomerStruct));



            }
        }
 
    }
}
