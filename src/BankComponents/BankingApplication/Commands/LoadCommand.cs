using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class LoadCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IPersistenceService>();
            accountService.Load();
        }
    }
}
