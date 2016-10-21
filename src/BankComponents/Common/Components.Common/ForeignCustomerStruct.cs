using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ForeignCustomerStruct
    {
        public int id;
        [MarshalAs(UnmanagedType.LPStr)]
        public string firstName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lastName;
    }
}