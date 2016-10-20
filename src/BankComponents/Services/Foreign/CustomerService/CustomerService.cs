using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
    }
}
