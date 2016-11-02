using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ForeignCustomerStruct
    {
        public int id;
        [MarshalAs(UnmanagedType.LPTStr)]
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string firstName;
        [MarshalAs(UnmanagedType.LPTStr)]
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string lastName;
    }
}