﻿using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Foreign
{
    public class CustomerWrapper
    {
        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int CreateCustomer([In, Out] ForeignCustomer customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DeleteCustomer(int id);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int updateCustomer(int id, [In, Out] ForeignCustomer customer);

        [DllImport(Common.DllNames.ForeignCustomerModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetCustomerById(int id, ForeignCustomerOut customer);

    }
}
