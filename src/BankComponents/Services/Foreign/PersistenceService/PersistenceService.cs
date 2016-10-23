using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(IPersistenceService))]
    public class PersistenceService : IPersistenceService
    {
        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }
    }
}
