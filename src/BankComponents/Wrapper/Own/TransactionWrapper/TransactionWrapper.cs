using System.Collections.Generic;
using System.Linq;
using Components.Common;

using tw = Components.Wrapper.Own.InternalTransactionWrapper;

namespace Components.Wrapper.Own
{
    public static class TransactionWrapper
    {
        public static void PayOut(int disposerId, int accountNumber, double amount, OwnCurrency currency)
        {
            SaveApiCaller.ExecuteCall(
                () => tw.PayOut(disposerId, accountNumber, amount, currency));
        }

        public static void PayIn(int disposerId, int accountNumber, double amount, OwnCurrency currency)
        {
            SaveApiCaller.ExecuteCall(() => tw.PayIn(disposerId, accountNumber, amount, currency));
        }

        public static void Transfer(int disposerId, int fromAccountNumber, int toAccountNumber, double amount,
            OwnCurrency currency)
        {
            SaveApiCaller.ExecuteCall(() => tw.Transfer(disposerId, fromAccountNumber, toAccountNumber, amount,currency));
        }
        
        public static IEnumerable<Transaction> AccountStatement(int disposerId, int accountNumber)
        {
            int numOfEntries = 0;
            SaveApiCaller.ExecuteCall(() => tw.AccountStatement(disposerId, accountNumber, null, out numOfEntries));
            var transactions = new TransactionStruct[numOfEntries];
            SaveApiCaller.ExecuteCall(
                () => tw.AccountStatement(disposerId, accountNumber, transactions, out numOfEntries));

            return transactions.Select(Transaction.FromStruct).ToList();
        }

        public static double AccountBalancing(int disposerId, int accountNumber, OwnCurrency currency)
        {
            double balance = -1;
            SaveApiCaller.ExecuteCall(() => tw.AccountBalancing(disposerId, accountNumber, currency, out balance));
            return balance;
        }
    }
}