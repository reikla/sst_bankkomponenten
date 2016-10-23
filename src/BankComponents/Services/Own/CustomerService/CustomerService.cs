using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(ICustomerService))]
    public class CustomerService : ICustomerService
    {
        public void CreateCustomer()
        {
            Console.WriteLine("Enter First Name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter Street: ");
            var street = Console.ReadLine();
            Console.WriteLine("Enter Zip: ");
            var zipString = Console.ReadLine();
            // ReSharper disable once AssignNullToNotNullAttribute
            int zip = int.Parse(zipString);

            var id = CustomerWrapper.CreateCustomer(firstName, lastName, street, zip);

            Console.WriteLine($"Customer with id '{id}' created.");
        }

        public void DeleteCustomer()
        {
            Console.WriteLine("Enter customer Id: ");
            var idString = Console.ReadLine();
            var id = int.Parse(idString);
            CustomerWrapper.DeleteCustomer(id);
            Console.WriteLine($"Customer with id '{id}' deleted.");
        }

        public void ModifyCustomer()
        {
            Console.WriteLine("Enter customer Id: ");
            var idString = Console.ReadLine();
            var id = int.Parse(idString);

            Console.WriteLine("Enter new first name - nothing to skip: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter new last name - nothing to skip: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("Enter new street - nothing to skip: ");
            var street = Console.ReadLine();

            Console.WriteLine("Enter new street - nothing to skip: ");
            var zipString = Console.ReadLine();
            var zip = int.Parse(zipString);

            CustomerWrapper.ModifyCustomer(id, firstName, lastName, street, ref zip);
        }
    }
}
