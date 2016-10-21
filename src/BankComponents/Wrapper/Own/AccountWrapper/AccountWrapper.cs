using System.Runtime.InteropServices;

namespace Components.Wrapper.Own
{
    public static class AccountWrapper
    {
        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateAccount(int disposerId, string accountName, AccountType accountType, out int accountNumber);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseAccount(int disposerId, int accountNumber);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddDisposer(int disposerId, int accountNumber, int newDisposerId);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RemoveDisposer(int disposerId, int accountNumber, int disposerToRemove);
    }
}
