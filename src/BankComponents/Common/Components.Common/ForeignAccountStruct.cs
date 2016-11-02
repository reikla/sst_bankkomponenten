using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ForeignAccountStruct
    {
        public int id;
        public string iban;
        public string bic;
        public bool isActive;
        public int customerId;
        public ForeignAccountType type;
    }
}