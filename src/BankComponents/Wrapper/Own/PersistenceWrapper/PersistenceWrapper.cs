using Components.Common;

using pw = Components.Wrapper.Own.InternalPersistenceWrapper;

namespace Components.Wrapper.Own
{
    public static class PersistenceWrapper
    {
        public static void Load()
        {
            SaveApiCaller.ExecuteCall(pw.Load);
        }

        public static void Store()
        {
            SaveApiCaller.ExecuteCall(pw.Store);
        }
    }
}