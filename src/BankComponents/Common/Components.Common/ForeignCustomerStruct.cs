using System.Runtime.InteropServices;

namespace Components.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ForeignCustomerStruct
    {
        int id;
        string firstName;
        string lastName;
    }
}