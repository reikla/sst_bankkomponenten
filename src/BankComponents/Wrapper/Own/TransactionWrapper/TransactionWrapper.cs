using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Own
{
    public class TransactionWrapper
    {
        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PayOut(int disposerId, int accountNumber, double amount, OwnCurrency ownCurrency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PayIn(int disposerId, int accountNumber, double amount, OwnCurrency currency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Transfer(int disposerId, int fromAccountNumber, int toAccountNumber, double amount, OwnCurrency currency);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AccountStatement(int disposerId, int accountNumber, Transaction[] data, out int numberOfEntries);

        [DllImport(DllNames.OwnTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AccountBalancing(int disposerId, int accountNumber, OwnCurrency currency, out double balance);
    }
}
