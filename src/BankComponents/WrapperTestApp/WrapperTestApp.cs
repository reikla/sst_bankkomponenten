namespace Components.App.WrapperTestApp
{
    class WrapperTestApp
    {
        static void Main(string[] args)
        {
            int id;
            int zip = 5202;
            int accountNumber;

//            Console.WriteLine($"Create Customer {InternalCustomerWrapper.CreateCustomer("Max", "Müller", "Hintertupfing", 5020, out id)}");
//            Console.WriteLine($"Create Customer {InternalCustomerWrapper.CreateCustomer("Reimar", "Klammer", "Hintertupfing", 5020, out id)}");
//            Console.WriteLine($"Create Customer {InternalCustomerWrapper.CreateCustomer("Reimar", "Klammer", "Hintertupfing", 5020, out id)}");
//            Console.WriteLine($"Modify Customer {InternalCustomerWrapper.ModifyCustomer(0, "Reimar", "Klammer", "Daheim", ref zip)}");
//            Console.WriteLine($"Modify Customer {InternalCustomerWrapper.ModifyCustomer(0, null, "", "Daheim", ref zip)}");
//            Console.WriteLine($"Delete Customer {InternalCustomerWrapper.DeleteCustomer(0)}");
//
//            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.LoanAccount, out accountNumber)}");
//            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.SavingsAccount, out accountNumber)}");
//            Console.WriteLine($"Create Account {AccountWrapper.CreateAccount(1, "HansWurst", AccountType.SavingsAccount, out accountNumber)}");
//            Console.WriteLine($"Close Account {AccountWrapper.CloseAccount(1,1)}");
//            Console.WriteLine($"AddDisposer Account {AccountWrapper.AddDisposer(1,0,2)}");
//            Console.WriteLine($"Remove Disposer {AccountWrapper.RemoveDisposer(1,0,2)}");
//
//
//            Console.WriteLine($"SetCurrencyToEuroFactor {CurrencyTranslationWrapper.SetCurrencyToEuroFactor(OwnCurrency.USD,1)}");
//
//
//            Console.WriteLine($"PayIn {TransactionWrapper.PayIn(1,0,100,OwnCurrency.EUR)}");
//            Console.WriteLine($"Transfer {TransactionWrapper.Transfer(1,0,2,50,OwnCurrency.EUR)}");
//
//            int numberofentries = -1;
//            var neededMemory = TransactionWrapper.AccountStatement(1, 0, null, out numberofentries);
//            
//            TransactionStruct[] transaction = new TransactionStruct[numberofentries];
//            Console.WriteLine($"AccountStatement {TransactionWrapper.AccountStatement(1, 0, transaction, out numberofentries)}");

           
            //accountNumber = AccountWrapper.CreateAccount(0, "Mein Account", AccountType.LoanAccount);

//             id = CustomerWrapper.CreateCustomer("Reimar", "Klammer", "Werkstaettenstrasse 8", 5020);
//             var account = AccountWrapper.CreateAccount(0, "Gehaltskonto", AccountType.LoanAccount);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayOut(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayOut(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
//             TransactionWrapper.PayIn(0,0,100,OwnCurrency.EUR);
// 
//             foreach (var transaction in TransactionWrapper.AccountStatement(0,0))
//             {
//                 Console.WriteLine($"Transaction Amount: {transaction.Amount} Type: {transaction.TransactionType}");
//             }
// 
//             Console.WriteLine($"Acctual balance: {TransactionWrapper.AccountBalancing(0,0,OwnCurrency.EUR)} {OwnCurrency.EUR}");
//             Console.WriteLine($"Created Customer with id {id}");


//             var customerService = new CustomerService();
//             try
//             {
//                 customerService.CreateCustomer("Reimar", "Klammer", "Werkstaettenstrasse 8", 5020);
//                 customerService.DeleteCustomer(1);
//             }
//             catch (ComponentException e)
//             {
//                 Console.WriteLine($"Something went wrong. Exception: '{e.GetType().Name}' Message '{e.Message}.");
//             }

        }
    }
}
