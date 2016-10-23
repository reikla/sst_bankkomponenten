using System.Runtime.InteropServices;

namespace Components.Wrapper.Own
{
    internal static class InternalAccountWrapper
    {
        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateAccount(int disposerId, string accountName, AccountType accountType, out int accountNumber);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CloseAccount(int disposerId, int accountNumber);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AddDisposer(int disposerId, int accountNumber, int newDisposerId);

        [DllImport(Common.DllNames.OwnAccountModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RemoveDisposer(int disposerId, int accountNumber, int disposerToRemove);

    }
}
