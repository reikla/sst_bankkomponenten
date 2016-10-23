using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public void CreateCustomer()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer()
        {
            throw new System.NotImplementedException();
        }

        public void ModifyCustomer()
        {
            throw new System.NotImplementedException();
        }
    }
}
