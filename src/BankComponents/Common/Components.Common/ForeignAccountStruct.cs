using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ForeignAccountStruct
    {
        int id;
        string iban;
        string bic;
        bool isActive;
        int customerId;
        ForeignAccountType type;
    }
}