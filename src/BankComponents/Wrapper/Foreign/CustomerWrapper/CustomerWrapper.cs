using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Foreign
{
    public class CustomerWrapper
    {
        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateCustomer(ref ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int updateCustomer(int id, ref ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetGetCustomerById(int id, ref ForeignCustomerStruct customer);

    }
}
