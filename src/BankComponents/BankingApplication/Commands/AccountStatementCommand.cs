using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class AccountStatementCommand : ICommand
    {
        public void Execute()
        {
            var transactionService = ServiceLocator.Instance().GetService<ITransactionService>();
            transactionService.AccountStatement();
        }
    }
}
