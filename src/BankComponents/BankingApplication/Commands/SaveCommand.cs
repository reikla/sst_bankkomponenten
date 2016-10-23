using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class SaveCommand : ICommand
    {
        public void Execute()
        {
            var accountService = ServiceLocator.Instance().GetService<IPersistenceService>();
            accountService.Save();
        }
    }
}
