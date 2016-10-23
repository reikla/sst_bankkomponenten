using BankingApplication.Commands;

namespace BankingApplication.Menu
{
    public class DividerMenuEntry : IMenuEntry
    {
        public ICommand Command => null;
        public string Title => "===============================================";
        public bool CanHandle(string selection)
        {
            return false;
        }
    }
}