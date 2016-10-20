using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(IPersistenceService))]
    public class PersistenceService : IPersistenceService
    {

    }
}
