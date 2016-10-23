using BankingApplication.Menu;

namespace BankingApplication.Commands
{
    class ExitCommand : ICommand
    {
        public void Execute()
        {
            throw new ExitException();
        }
    }
}
