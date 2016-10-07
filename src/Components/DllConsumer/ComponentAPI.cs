using System.Runtime.InteropServices;

namespace DllConsumer
{
    class ComponentApi
    {
        [DllImport("CComponent.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetNumber();

        [DllImport("CComponent.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ManipulateStruct(ref MyStruct myStruct);


    }
}
