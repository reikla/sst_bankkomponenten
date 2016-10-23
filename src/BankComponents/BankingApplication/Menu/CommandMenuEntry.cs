
using BankingApplication.Commands;

namespace BankingApplication.Menu
{
    class CommandMenuEntry : IMenuEntry
    {

        private static int IdStore = 0;

        private string _title;


        public CommandMenuEntry(string title, ICommand command)
        {
            _title = title;
            Command = command;
            Id = $"{IdStore++}";
        }

        public bool CanHandle(string selection)
        {
            return Id.StartsWith(selection);
        }

        private string Id { get; }
        public string Title => $"{Id}. {_title}.";
        public ICommand Command { get; }

    }
}
