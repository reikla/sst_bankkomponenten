using System;
using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class ForeignAccountOut
    {
        public int id;
        public IntPtr iban;
        public IntPtr bic;
        public int isActive;
        public int customerId;
        public ForeignAccountType type;
    }
}
