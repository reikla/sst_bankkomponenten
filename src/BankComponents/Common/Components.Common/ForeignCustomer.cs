
using System;
using System.Runtime.InteropServices;

namespace Components.Common
{
    //[Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class ForeignCustomer
    {
        public int id;
        [MarshalAs(UnmanagedType.LPStr)]
        public string firstName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lastName;
    }
}
