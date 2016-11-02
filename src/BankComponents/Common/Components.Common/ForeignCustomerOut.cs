
using System;
using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class ForeignCustomerOut
    {
        public int id { set; get; }
        public IntPtr firstName { set; get; }
        public IntPtr lastName { set; get; }
    }
}
