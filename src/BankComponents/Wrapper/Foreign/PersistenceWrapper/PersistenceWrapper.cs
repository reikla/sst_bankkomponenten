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
        public static extern ForeignCustomerStruct SelectCustomerById(int id, [Out] out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertCustomer([In, Out] ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int UpdateCustomer([In, Out] ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignAccountStruct SelectAccountById(int id, [Out] out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignAccountStruct[] GetAccountsByCustomerId(int id, [Out] out int accountCount, out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertAccount([In, Out] ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteAccount(int id);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int UpdateAccount([In, Out] ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InsertTransaction([In, Out] ForeignTransactionStruct transaction);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignTransactionStruct[] GetTransactionsForSourceAccountId(int accountId, [Out] out int resultLength, out int errorCode);

        [DllImport(Common.DllNames.ForeignPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern ForeignTransactionStruct[] GetTransactionsForDestinationAccountId(int accountId, [Out] out int resultLength, out int errorCode);
    }
}
