using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Own
{
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void CreateAccount()
        {
            Console.WriteLine("Enter disposer ID: ");
            var disposerId = Console.ReadLine();
            Console.WriteLine("Enter Account Name ");
            var accountName = Console.ReadLine();
            Console.WriteLine("Enter AccountType SavingsAccount=1 LoadAccount=2: ");
            var typeString = Console.ReadLine();
            
            Console.WriteLine("Enter disposer ID: ");
        }

        public void CloseAccount()
        {
            throw new NotImplementedException();
        }

        public void AddDisposer()
        {
            throw new NotImplementedException();
        }

        public void RemoveDisposer()
        {
            throw new NotImplementedException();
        }
    }
}
