using System.Runtime.InteropServices;

namespace Components.Wrapper.Own
{
    internal static class InternalCustomerWrapper
    {
        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CreateCustomer(string firstName, string lastName, string street, int zip, out int id);

        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ModifyCustomer(int id, string firstName, string lastName, string street, int zip);
    }
}
