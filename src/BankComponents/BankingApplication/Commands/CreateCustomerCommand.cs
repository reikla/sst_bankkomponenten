using System;
using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public void Execute()
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

            var customerService = ServiceLocator.Instance().GetService<ICustomerService>();
            var customerId = customerService.CreateCustomer(firstName, lastName, street, zip);
            Console.WriteLine($"Customer with Id {customerId} created.");
        }
    }
}
