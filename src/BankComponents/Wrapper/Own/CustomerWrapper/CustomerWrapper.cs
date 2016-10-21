using System.Runtime.InteropServices;

namespace Components.Wrapper.Own
{
    public static class CustomerWrapper
    {
        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateCustomer(string firstName, string lastName, string street, int zip, out int id);

        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.OwnCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ModifyCustomer(int id, string firstName, string lastName, string street, ref int zip);
    }
}
