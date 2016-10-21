using System;
using Components.Common;
using System.Runtime.InteropServices;

namespace Components.Wrapper.Foreign
{
    public class PersistenceWrapper
    {
        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ConnectDataAccessLayer();

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CommitDataAccessLayer();

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RollbackDataAccessLayer();

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SelectCustomerById(int id, [Out] out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertCustomer(ref ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int UpdateCustomer(ref ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignAccountStruct SelectAccountById(int id, [Out] out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetAccountsByCustomerId(int id, [Out] out int accountCount, out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertAccount(ref ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteAccount(int id);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int UpdateAccount(ref ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertTransaction(ref ForeignTransactionStruct transaction);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignTransactionStruct[] GetTransactionsForSourceAccountId(int accountId, [Out] out int resultLength, out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignTransactionStruct[] GetTransactionsForDestinationAccountId(int accountId, [Out] out int resultLength, out int errorCode);
    }
}
