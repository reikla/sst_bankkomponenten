using System;
using System.ComponentModel.Composition;
using Components.Common;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(ITransactionService))]
    public class TransactionService : ITransactionService
    {
        public void PayOut()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = InputParser.GetCurrency();

            TransactionWrapper.PayOut(disposerId, accountNumber, amount, currency);

            Console.WriteLine($"Successfully payed out '{amount}' {currency}.");
        }

        public void PayIn()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = InputParser.GetCurrency();

            TransactionWrapper.PayIn(disposerId, accountNumber, amount, currency);

            Console.WriteLine($"Successfully payed in '{amount}' {currency}.");
        }

        public void Transfer()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var sourceAccountNumber = InputParser.GetIntInput("Enter source account number: ", "Account number source", i => i >= 0);
            var targetAccountNumber = InputParser.GetIntInput("Enter target account number: ", "Account number target", i => i >= 0);
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var currency = InputParser.GetCurrency();

            TransactionWrapper.Transfer(disposerId, sourceAccountNumber, targetAccountNumber, amount, currency);

            Console.WriteLine($"Successfully transfered '{amount}' '{currency}' from '{sourceAccountNumber}' to '{targetAccountNumber}'.");
        }

        public void AccountStatement()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);

            var transactions = TransactionWrapper.AccountStatement(disposerId, accountNumber);

            foreach (var transaction in transactions)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.BarPayIn:
                        Console.WriteLine($"Payed in '{transaction.Amount}' {transaction.Currency}.");
                        break;
                    case TransactionType.BarPayOut:
                        Console.WriteLine($"Payed out '{transaction.Amount}' {transaction.Currency}.");
                        break;
                    case TransactionType.Transaction:
                        if(transaction.FromAccountNumber == accountNumber)
                            Console.WriteLine($"Transfered '{transaction.Amount}' {transaction.Currency} to {transaction.ToAccountNumber}.");
                        else
                            Console.WriteLine($"Transfered '{transaction.Amount}' {transaction.Currency} from {transaction.FromAccountNumber}.");
                        break;
                }
            }
        }

        public void AccountBalancing()
        {
            var disposerId = InputParser.GetIntInput("Enter disposer ID: ", "Disposer Id", i => i >= 0);
            var accountNumber = InputParser.GetIntInput("Enter account number: ", "Account number", i => i >= 0);
            var currency = InputParser.GetCurrency();

            var balance = TransactionWrapper.AccountBalancing(disposerId, accountNumber, currency);

            Console.WriteLine($"Current balance: '{balance}' {currency}.");
        }
    }
}
