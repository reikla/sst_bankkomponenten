using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class RemoveRemoteTransactionCommand : ICommand
    {
        public void Execute()
        {
            var service = ServiceLocator.Instance().GetService<IRemoteBankService>();
            service.RemoveOpenRemoteTransactions();
        }
    }
}
