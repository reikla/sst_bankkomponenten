using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;

using account_service = Components.Wrapper.Foreign.ExternalAccountWrapper;
using Components.Common;

namespace Components.Service.Foreign
{
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void CreateAccount()
        {
            ForeignAccount account = new ForeignAccount();
            account.customerId = InputParser.GetIntInput("Enter customer ID: ", "Customer Id", i => i >= 0);
            account.bic = InputParser.GetStringInput("Enter BIC: ", "BIC", s => (!string.IsNullOrWhiteSpace(s)));
            account.iban = InputParser.GetStringInput("Enter IBAN: ", "IBAN", s => (!string.IsNullOrWhiteSpace(s)));//&(s.Length<=35 & s.Length>=15));
            account.isActive = true;
            account.type = InputParser.GetForeignAccountTypeInput();

            account_service.CreateAccount(account);
            Console.WriteLine($"Created Account. Account Id: '{account.id}'.");
        }

        public void CloseAccount()
        {
            var accountId = InputParser.GetIntInput("Enter account ID: ", "Account Id", i => i >= 0);
            account_service.CloseAccount(accountId);
            Console.WriteLine($"Account with id '{accountId}' closed.");
        }

        public void AddDisposer()
        {
            int customerId = 0;
            ForeignAccount account = null;
            account_service.AddDisposer(customerId, account);
        }

        public void RemoveDisposer()
        {
            account_service.RemoveDisposer();
        }
    }
}
