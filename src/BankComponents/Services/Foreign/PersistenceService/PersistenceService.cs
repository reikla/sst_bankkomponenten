using System.ComponentModel.Composition;
using Components.Contracts.Services;

using persistence_service = Components.Wrapper.Foreign.ExternalPersistenceWrapper;

namespace Components.Service.Foreign
{
    [Export(typeof(IPersistenceService))]
    public class PersistenceService : IPersistenceService
    {
        public void Save()
        {
            persistence_service.Store();
        }

        public void Load()
        {
            persistence_service.Load();
        }
    }
}
