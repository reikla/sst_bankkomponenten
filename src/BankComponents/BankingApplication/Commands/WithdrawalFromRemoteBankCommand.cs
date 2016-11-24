using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class WithdrawalFromRemoteBankCommand : ICommand
    {
        public void Execute()
        {
            var remoteBankingService = ServiceLocator.Instance().GetService<IRemoteBankService>();
            remoteBankingService.Withdrawl();
        }
    }
}