using Components.Common;

using aw = Components.Wrapper.Foreign.AccountWrapper;

namespace Components.Wrapper.Foreign
{
    public static class ExternalAccountWrapper
    {
        public static int CreateAccount(ForeignAccount account)
        {
            int accountNumber = 0;
            ForeignSaveApiCaller.ExecuteCall(() => aw.createAccount(account));
            accountNumber = account.id;
            return accountNumber;
        }

        public static void CloseAccount(int accountId)
        {
            ForeignSaveApiCaller.ExecuteCall(() => aw.closeAccount(accountId));
        }

        /// <summary>
        /// As the accessor handling is still missing in the foreign dll, 
        /// an exception is thrown automiatically and the ddl call is commented out.
        ///</summary>
        public static void AddDisposer(int customerId, ForeignAccount account)
        {
            //ForeignSaveApiCaller.ExecuteCall(() => aw.addAccessor(customerId, account));
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.ACCOUNT_FEATURE_NOT_SUPPORTED);
        }

        /// <summary>
        /// As there exists no method in the foreign dll to remove a disposer/accessor from an account
        /// an exception is thrown automatically.
        /// </summary>
        public static void RemoveDisposer()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.ACCOUNT_FEATURE_NOT_SUPPORTED);
        }

    }
}
