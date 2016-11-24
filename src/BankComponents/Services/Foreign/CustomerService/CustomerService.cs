using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Common;
using customer_service = Components.Wrapper.Foreign.ExternalCustomerWrapper;
using System;

namespace Components.Service.Foreign
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public void CreateCustomer()
        {
            ForeignCustomer customer = new ForeignCustomer();
            customer.firstName = InputParser.GetStringInput("Enter first name: ", "First Name", s => !string.IsNullOrWhiteSpace(s));
            customer.lastName = InputParser.GetStringInput("Enter last name: ", "Last Name", s => !string.IsNullOrWhiteSpace(s));
            customer_service.CreateCustomer(customer);
            Console.WriteLine($"Customer with id '{customer.id}' created.");
        }

        public void DeleteCustomer()
        {
            int customerId = 0;
            customer_service.DeleteCustomer(customerId);
            //Console.WriteLine($"Customer with id '{customerId}' deleted.");
        }

        public void ModifyCustomer()
        {
            ForeignCustomer customer = new ForeignCustomer();
            customer_service.ModifyCustomer(customer);
            //Console.WriteLine($"Customer '{customer.id}' successfully modified.");
        }
    }
}
