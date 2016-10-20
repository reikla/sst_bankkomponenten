using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void Foo()
        {
            Console.WriteLine($"Foo from {GetType().FullName}!");
        }
    }
}
