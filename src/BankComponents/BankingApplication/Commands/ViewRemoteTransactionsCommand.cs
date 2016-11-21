using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class ViewRemoteTransactionsCommand : ICommand
    {
        public void Execute()
        {
            var remoteBankingService = ServiceLocator.Instance().GetService<IRemoteBankService>();
            remoteBankingService.ViewOpenRemoteTransactions();
        }
    }
}