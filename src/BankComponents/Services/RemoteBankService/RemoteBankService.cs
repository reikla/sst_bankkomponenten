using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;
using ForeignBankComponent;

namespace Components.Service.RemoteBankService
{
    [Export(typeof(IRemoteBankService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RemoteBankService : IRemoteBankService
    {
        private IRemoteBankComponent _remoteBank;

        public RemoteBankService()
        {
            _remoteBank = new RemoteBankComponent("Email", "Password");
        }

        public void Transfer()
        {
            Console.WriteLine("Transfering to remote Account");
        }

        public void Withdrawl()
        {
            Console.WriteLine("Withdrawal from remote Bank");
        }

        public void ViewRemoteTransactions()
        {
            Console.WriteLine("List of remote transactions.");
        }
    }
}