using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Own
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Transaction
    {
       double amount;
       double factor;
       OwnCurrency currency;
       int fromAccount;
       int toAccount;
       int disposer;
    }
}