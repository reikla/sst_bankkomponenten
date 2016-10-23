using BankingApplication.Commands;

namespace BankingApplication.Menu
{
    public interface IMenuEntry
    {
        ICommand Command { get; }
        string Title { get; }

        bool CanHandle(string selection);
    }
}