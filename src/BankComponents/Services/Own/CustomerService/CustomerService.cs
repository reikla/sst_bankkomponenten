using System;
using System.ComponentModel.Composition;
using Components.Common;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public void CreateCustomer()
        {
            var firstName = InputParser.GetStringInput("Enter First Name: ", "First Name",
                s => !string.IsNullOrWhiteSpace(s));
            var lastName = InputParser.GetStringInput("Enter last Name: ", "Last Name",
                s => !string.IsNullOrWhiteSpace(s));

            var street = InputParser.GetStringInput("Enter street: ", "Street", s => !string.IsNullOrWhiteSpace(s));
            var zip = InputParser.GetIntInput("Enter Zip: ", "Zip", i => (i>=1000 && i<10000));
            var id = CustomerWrapper.CreateCustomer(firstName, lastName, street, zip);

            Console.WriteLine($"Customer with id '{id}' created.");
        }

        public void DeleteCustomer()
        {
            var customerId = InputParser.GetIntInput("Enter customer ID: ", "Customer Id", i => i >= 0);


            CustomerWrapper.DeleteCustomer(customerId);
            Console.WriteLine($"Customer with id '{customerId}' deleted.");
        }

        public void ModifyCustomer()
        {
            var customerId = InputParser.GetIntInput("Enter customer ID: ", "Customer Id", i => i >= 0);
            var firstName = InputParser.GetStringInput("Enter first name - nothing to skip: ", "First Name");
            var lastName = InputParser.GetStringInput("Enter last name - nothing to skip:", "Last Name");
            var street = InputParser.GetStringInput("Enter street - nothing to skip: ", "Street");


            var zipString = InputParser.GetStringInput("Enter new zip - nothing to skip: ", "Zip");
            int zip = -1;
            if (!string.IsNullOrWhiteSpace(zipString))
            {
                int.TryParse(zipString, out zip);
            }

            CustomerWrapper.ModifyCustomer(customerId, firstName, lastName, street, zip);

            Console.WriteLine($"Customer '{customerId}' successfully modified.");
        }
    }
}
