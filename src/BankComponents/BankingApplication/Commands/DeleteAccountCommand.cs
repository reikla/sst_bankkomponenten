using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class DeleteAccountCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IAccountService>();
            accountService.CloseAccount();
        }
    }
}
