using System.Runtime.InteropServices;
using System.Text;

namespace DllConsumer
{
    public struct MyStruct
    {
        public int size;
        [MarshalAsAttribute(UnmanagedType.LPTStr)]
        public string mystr;
    }
}