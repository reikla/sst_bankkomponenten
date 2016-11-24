using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class TransferToRemoteBankCommand : ICommand
    {
        public void Execute()
        {
            var remoteBankingService = ServiceLocator.Instance().GetService<IRemoteBankService>();
            remoteBankingService.Transfer();
        }
    }
}