using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class AccountBalanceCommand : ICommand
    {
        public void Execute()
        {
            var transactionService = ServiceLocator.Instance().GetService<ITransactionService>();
            transactionService.AccountBalancing();
        }
    }
}
