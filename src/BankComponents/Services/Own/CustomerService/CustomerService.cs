using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public int CreateCustomer(string firstName, string lastName, string street, int zip)
        {
            return CustomerWrapper.CreateCustomer(firstName, lastName, street, zip);
        }

        public void DeleteCustomer(int customerId)
        {
            CustomerWrapper.DeleteCustomer(customerId);
        }

        public void ModifyCustomer(int customerId, string firstName, string lastName, string street, int zip)
        {
            CustomerWrapper.ModifyCustomer(customerId, firstName, lastName, street, ref zip);
        }
    }
}
