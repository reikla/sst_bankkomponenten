using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class TransferCommand : ICommand
    {
        public void Execute()
        {
            var transactionService = ServiceLocator.Instance().GetService<ITransactionService>();
            transactionService.Transfer();
        }
    }
}
