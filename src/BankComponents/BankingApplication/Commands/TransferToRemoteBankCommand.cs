using System;

namespace BankingApplication.Commands
{
    public class TransferToForeignBankCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Transfering to foreign Account");
        }
    }
}