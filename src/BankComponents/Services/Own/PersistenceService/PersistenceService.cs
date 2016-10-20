using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Own
{
    [Export(typeof(IPersistenceService))]
    public class PersistenceService : IPersistenceService
    {
    }
}
