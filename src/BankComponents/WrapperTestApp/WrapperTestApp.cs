using System;
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
            Console.WriteLine($"Close Account {AccountWrapper.CloseAccount(1,1)}");
            Console.WriteLine($"AddDisposer Account {AccountWrapper.AddDisposer(1,0,2)}");
            Console.WriteLine($"Remove Disposer {AccountWrapper.RemoveDisposer(1,0,2)}");




        }
    }
}
