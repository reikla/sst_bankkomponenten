using System;
using System.ComponentModel.Composition;
using Components.Common;
using Components.Contracts.Services;
using Components.Wrapper.Own;
using AccountType = Components.Contracts.AccountType;

namespace Components.Service.Own
{
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void CreateAccount()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountName = InputParser.GetStringInput("Enter account Name: ", "Account Name", s => (!string.IsNullOrWhiteSpace(s)));
            var accountType = InputParser.GetAccountTypeInput();

            var accountNumber = AccountWrapper.CreateAccount(disposerId, accountName, accountType == AccountType.LoanAccount ? Wrapper.Own.AccountType.LoanAccount : Wrapper.Own.AccountType.SavingsAccount);

            Console.WriteLine($"Created Account. Account number: '{accountNumber}'.");
        }

        public void CloseAccount()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);

            AccountWrapper.CloseAccount(disposerId, accountNumber);
            Console.WriteLine($"Account '{accountNumber}' closed.");
        }

        public void AddDisposer()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);
            var newDisposerId = InputParser.GetIntInput("Enter new disposer ID: ", "New disposer Id", i => i >= 0);

            AccountWrapper.AddDisposer(disposerId, accountNumber, newDisposerId);

            Console.WriteLine($"Disposer {newDisposerId} added to account '{accountNumber}'");
        }

        public void RemoveDisposer()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);
            var toRemoveDisposerId = InputParser.GetIntInput("Enter ID of disposer to remove: ", "New disposer Id", i => i >= 0);

            AccountWrapper.RemoveDisposer(disposerId, accountNumber, toRemoveDisposerId);

            Console.WriteLine($"Disposer {toRemoveDisposerId} removed from account '{accountNumber}'");

        }
    }
}
