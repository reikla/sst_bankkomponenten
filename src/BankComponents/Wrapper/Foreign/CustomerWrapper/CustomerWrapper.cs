using System.Runtime.InteropServices;

namespace Components.Wrapper.Foreign
{
    public class CustomerWrapper
    {
        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateCustomer([In,Out] ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int updateCustomer(int id, [In, Out] ForeignCustomerStruct customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetGetCustomerById(int id, [In, Out] ForeignCustomerStruct customer);

    }
}
