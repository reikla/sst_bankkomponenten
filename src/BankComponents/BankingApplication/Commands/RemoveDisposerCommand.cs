using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class RemoveDisposerCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IAccountService>();
            accountService.RemoveDisposer();
        }
    }
}
