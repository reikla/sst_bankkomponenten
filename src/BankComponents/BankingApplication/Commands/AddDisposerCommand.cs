using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class AddDisposerCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IAccountService>();
            accountService.AddDisposer();
        }
    }
}
