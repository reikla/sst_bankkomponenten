using System.Runtime.InteropServices;

namespace Components.Wrapper.Own
{
    public static class InternalPersistenceWrapper
    {
        [DllImport(Common.DllNames.OwnPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Load();

        [DllImport(Common.DllNames.OwnPersistenceModule, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Store();
    }
}
