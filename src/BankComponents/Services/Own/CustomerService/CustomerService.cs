using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Own
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
    }
}
