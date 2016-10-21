using System;
using Components.Common;
using Components.Wrapper.Own;

namespace Components.App.WrapperTestApp
{
    class WrapperTestApp
    {
        static void Main(string[] args)
        {
            int id;
            int zip = 5202;
            int accountNumber;

            Console.WriteLine($"Create Customer {CustomerWrapper.CreateCustomer("Max", "Müller", "Hintertupfing", 5020, out id)}");
            Console.WriteLine($"Create Customer {CustomerWrapper.CreateCustomer("Reimar", "Klammer", "Hintertupfing", 5020, out id)}");
            Console.WriteLine($"Create Customer {CustomerWrapper.CreateCustomer("Reimar", "Klammer", "Hintertupfing", 5020, out id)}");
            Console.WriteLine($"Modify Customer {CustomerWrapper.ModifyCustomer(0, "Reimar", "Klammer", "Daheim", ref zip)}");
            Console.WriteLine($"Modify Customer {CustomerWrapper.ModifyCustomer(0, null, "", "Daheim", ref zip)}");
            Console.WriteLine($"Delete Customer {CustomerWrapper.DeleteCustomer(0)}");

            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.LoanAccount, out accountNumber)}");
            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.SavingsAccount, out accountNumber)}");
            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.SavingsAccount, out accountNumber)}");
            Console.WriteLine($"Close Account {AccountWrapper.CloseAccount(1,1)}");
            Console.WriteLine($"AddDisposer Account {AccountWrapper.AddDisposer(1,0,2)}");
            Console.WriteLine($"Remove Disposer {AccountWrapper.RemoveDisposer(1,0,2)}");


            Console.WriteLine($"SetCurrencyToEuroFactor {CurrencyTranslationWrapper.SetCurrencyToEuroFactor(OwnCurrency.USD,1)}");


            Console.WriteLine($"PayIn {TransactionWrapper.PayIn(1,0,100,OwnCurrency.EUR)}");
            Console.WriteLine($"Transfer {TransactionWrapper.Transfer(1,0,2,50,OwnCurrency.EUR)}");

            int numberofentries = -1;
            var neededMemory = TransactionWrapper.AccountStatement(1, 0, null, out numberofentries);
            
            Transaction[] transaction = new Transaction[numberofentries];
            Console.WriteLine($"AccountStatement {TransactionWrapper.AccountStatement(1, 0, transaction, out numberofentries)}");

        }
    }
}
