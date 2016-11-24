
using BankingApplication.Commands;

namespace BankingApplication.Menu
{
  class CommandMenuEntry : IMenuEntry
  {

    private static int IdStore = 0;

    private readonly string _title;

    private readonly ServiceType _availableIn;

    public CommandMenuEntry(string title, ICommand command, ServiceType availableIn)
    {
      _title = title;
      Command = command;
      Id = $"{IdStore++}";
      _availableIn = availableIn;
    }

    public bool CanHandle(string selection)
    {
      return Id.StartsWith(selection);
    }

    public bool IsAvailable(ServiceType selectedVersion)
    {
      return (_availableIn & selectedVersion) == selectedVersion;
    }

    private string Id { get; }
    public string Title => $"{Id}. {_title}.";
    public ICommand Command { get; }

  }
}
