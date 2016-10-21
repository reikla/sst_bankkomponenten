using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Foreign
{
    public static class AccountWrapper
    {
        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAccountById(int id, [Out] out ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetAccountsByCustomerId(int id, [Out] out int accountCount, out ForeignAccountStruct[] accounts);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int createAccount(ref ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int closeAccount(int id);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int updateAccount(int id, ref ForeignAccountStruct account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int addAccessor(int id, ref ForeignAccountStruct account);
    }
}
