using System;
using System.Runtime.InteropServices;
namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class ForeignAccount
    {
        public int id;
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 31)]
        public String iban;
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 11)]
        public String bic;
        public bool isActive;
        public int customerId;
        public ForeignAccountType type;
    }
}
