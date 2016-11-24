using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Foreign
{
    public static class AccountWrapper
    {
        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getAccountById(int id, ForeignAccountOut account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetAccountsByCustomerId(int id, out int accountCount, out ForeignAccount[] accounts);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int createAccount([In, Out] ForeignAccount account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int closeAccount(int id);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int updateAccount(int id, [In, Out] ForeignAccount account);

        [DllImport(Common.DllNames.ForeignAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int addAccessor(int id, [In,Out] ForeignAccount account);
    }
}
