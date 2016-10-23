using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Own
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TransactionStruct
    {
        public double amount;
        public double factor;
        public OwnCurrency currency;
        public int fromAccount;
        public int toAccount;
        public int disposer;
    }
}