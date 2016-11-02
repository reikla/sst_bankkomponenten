using Components.Common;
using System.Runtime.InteropServices;

namespace Components.Wrapper.Foreign
{
    public class TransactionWrapper
    {
        [DllImport(DllNames.ForeignTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int einzahlung(ForeignCurrency currency, double amount, int id);

        [DllImport(DllNames.ForeignTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int auszahlung(ForeignCurrency currency, double amount, int id);

        [DllImport(DllNames.ForeignTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ueberweisung(ForeignCurrency currency, double amount, int targetId, int sourceId);

        [DllImport(DllNames.ForeignTransactionModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int calcSaldo(int id, ForeignCurrency currency, out double saldo);

    }
}
