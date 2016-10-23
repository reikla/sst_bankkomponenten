using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Own
{
    internal static class InternalTransactionWrapper
    {
        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PayOut(int disposerId, int accountNumber, double amount, OwnCurrency ownCurrency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PayIn(int disposerId, int accountNumber, double amount, OwnCurrency currency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int Transfer(int disposerId, int fromAccountNumber, int toAccountNumber, double amount, OwnCurrency currency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AccountStatement(int disposerId, int accountNumber, [In,Out] TransactionStruct[] data, out int numberOfEntries);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AccountBalancing(int disposerId, int accountNumber, OwnCurrency currency, out double balance);
    }
}
