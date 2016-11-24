using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Common;
using transaction_service = Components.Wrapper.Foreign.ExternalTransactionWrapper;
using System;

namespace Components.Service.Foreign
{
    [Export(typeof(ITransactionService))]
    public class TransactionService : ITransactionService
    {
        public void PayOut()
        {
            var accountId = InputParser.GetIntInput("Enter account ID: ", "Account Id", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = ForeignCurrency.EUR; //as there are no other currencies supported it is set automatically to euro.
            transaction_service.PayOut(accountId, amount, currency);
            Console.WriteLine($"Successfully payed out '{amount}' {currency}.");
        }

        public void PayIn()
        {
            var accountId = InputParser.GetIntInput("Enter recipient account ID : ", "Account Id", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = ForeignCurrency.EUR; //as there are no other currencies supported it is set automatically to euro.
            transaction_service.PayIn(accountId, amount, currency);
            Console.WriteLine($"Successfully payed in '{amount}' {currency}.");
        }

        public void Transfer()
        {
            var sourceAccountId = InputParser.GetIntInput("Enter source account ID: ", "Source account ID ", i => i >= 0);
            var targetAccountId = InputParser.GetIntInput("Enter target account ID: ", "Target account ID ", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = ForeignCurrency.EUR;

            transaction_service.Transfer(currency, amount, targetAccountId, sourceAccountId);
            Console.WriteLine($"Successfully transfered '{amount}' '{currency}' from account with id '{sourceAccountId}' to account with id '{targetAccountId}'.");
        }

        public void AccountStatement()
        {
            transaction_service.AccountStatement();
        }

        public void AccountBalancing()
        {
            var accountId = InputParser.GetIntInput("Enter account ID: ", "Account ID", i => i >= 0);
            ForeignCurrency currency = ForeignCurrency.EUR;
            var balance = transaction_service.AccountBalancing(accountId, currency);
            Console.WriteLine($"Current balance: '{balance}' {currency}.");
        }
    }
}
