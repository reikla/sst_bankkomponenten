using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class CreateAccountCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IAccountService>();
            accountService.CreateAccount();
        }
    }
}
