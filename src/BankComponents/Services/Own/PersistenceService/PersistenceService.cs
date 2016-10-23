using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(IPersistenceService))]
    public class PersistenceService : IPersistenceService
    {
        public void Save()
        {
            PersistenceWrapper.Store();
            Console.WriteLine("Data successfully Stored.");
        }

        public void Load()
        {
            PersistenceWrapper.Load();
            Console.WriteLine("Data successfully Loaded.");
        }
    }
}
