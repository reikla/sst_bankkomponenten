using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ForeignTransactionStruct
    {
        int Id;
        bool IsDestinationAccountIdNull;
        int DestinationAccountId;
        bool IsSourceAccountIdNull;
        int SourceAccountId;
        ForeignCurrency Currency;
        double Amount;
    }
}
