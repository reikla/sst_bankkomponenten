using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class PayOutCommand : ICommand
    {
        public void Execute()
        {
            var transactionService = ServiceLocator.Instance().GetService<ITransactionService>();
            transactionService.PayOut();
        }
    }
}
