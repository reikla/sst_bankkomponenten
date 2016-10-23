using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public int CreateCustomer(string firstName, string lastName, string street, int zip)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public void ModifyCustomer(int customerId, string firstName, string lastName, string street, int zip)
        {
            throw new System.NotImplementedException();
        }
    }
}
