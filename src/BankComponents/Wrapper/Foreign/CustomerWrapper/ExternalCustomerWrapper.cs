using Components.Common;
using cw = Components.Wrapper.Foreign.CustomerWrapper;

namespace Components.Wrapper.Foreign
{
    public static class ExternalCustomerWrapper
    {
        public static int CreateCustomer(ForeignCustomer customer)
        {
            int customerId = 0;
            ForeignSaveApiCaller.ExecuteCall(
                () => cw.CreateCustomer(customer));
            customerId = customer.id;
            return customerId;
        }

        ///<summary>
        /// As this method of the foreign dll still causes a vector subscript out of range error,
        /// the method is commented out and an exception is automatically thrown instead.
        ///</summary>
        public static void DeleteCustomer(int id)
        {
            //ForeignSaveApiCaller.ExecuteCall(() => cw.DeleteCustomer(id));
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CUSTOMER_FEATURE_NOT_SUPPORTED);
        }

        ///<summary>
        /// As this method of the foreign dll still returns "customer does not exist" although he exists,
        /// the method is commented out and an exception is automatically thrown instead.
        ///</summary>
        public static void ModifyCustomer(ForeignCustomer customer)
        {
            //ForeignSaveApiCaller.ExecuteCall(() => cw.updateCustomer(customer.id, customer));
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CUSTOMER_FEATURE_NOT_SUPPORTED);
        }
    }
}
