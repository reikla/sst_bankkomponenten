using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class PayInCommand : ICommand
    {
        public void Execute()
        {
            var transactionService = ServiceLocator.Instance().GetService<ITransactionService>();
            transactionService.PayIn();
        }
    }
}
