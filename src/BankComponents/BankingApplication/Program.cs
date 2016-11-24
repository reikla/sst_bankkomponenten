using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using BankingApplication.Commands;
using BankingApplication.Menu;
using Components.Common.Exceptions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace BankingApplication
{
  class Program
  {
    private static ServiceType selectedService;

    static void Main(string[] args)
    {
<<<<<<< HEAD
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();


            var height = Console.WindowHeight;
            var width = Console.WindowWidth;

            height += height/2;

            Console.SetWindowSize(width,height);

            ConfigureLogging();
            logger.Info("Starting application");
=======
      var logger = LogManager.GetCurrentClassLogger();
      ConfigureLogging();
      logger.Info("Starting application");
>>>>>>> 919463224e42b860a58f26d1d3bd146e30d07d46

      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

      AggregateCatalog serviceCatalog = new AggregateCatalog();

      selectedService = AskForServiceType();

      foreach (var assembly in AssemblyLocator.GetServiceAssemblies(selectedService))
      {
        serviceCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
      }

      CompositionContainer container = new CompositionContainer(serviceCatalog);

      container.ComposeParts();

      ServiceLocator.Instance().Init(container);

      var menuEntries = new List<IMenuEntry>
            {
                new CommandMenuEntry("Create customer", new CreateCustomerCommand(),ServiceType.All),
                new CommandMenuEntry("Modify customer", new ModifyCustomerCommand(),ServiceType.All),
                new CommandMenuEntry("Delete customer", new DeleteCustomerCommand(),ServiceType.All),
                new DividerMenuEntry(),
                new CommandMenuEntry("Create account", new CreateAccountCommand(), ServiceType.All),
                new CommandMenuEntry("Delete account", new DeleteAccountCommand(),ServiceType.All),
                new CommandMenuEntry("Add disposer from account", new AddDisposerCommand(),ServiceType.All),
                new CommandMenuEntry("Remove disposer from account", new RemoveDisposerCommand(),ServiceType.All),
                new DividerMenuEntry(),
                new CommandMenuEntry("Set currency to Euro factor", new SetCurrencyToEuroFactorCommand(), ServiceType.All),
                new CommandMenuEntry("Get currency from Euro factor", new GetCurrencyToEuroFactorCommand(), ServiceType.All),
                new CommandMenuEntry("Translate to Euro", new TranslateToEuroCommand() ,ServiceType.All),
                new CommandMenuEntry("Translate from Euro", new TranslateFromEuroCommand(), ServiceType.All),
                new DividerMenuEntry(),
                new CommandMenuEntry("Pay out", new PayOutCommand(), ServiceType.All),
                new CommandMenuEntry("Pay in", new PayInCommand(), ServiceType.All),
                new CommandMenuEntry("Transfer money", new TransferCommand(), ServiceType.All),
                new CommandMenuEntry("Account statement", new AccountStatementCommand(), ServiceType.All),
                new CommandMenuEntry("Account balance", new AccountBalanceCommand(),ServiceType.All),
                new DividerMenuEntry(),
                new CommandMenuEntry("Load Data", new LoadCommand(),ServiceType.All),
                new CommandMenuEntry("Save Data", new SaveCommand(),ServiceType.All),
                new DividerMenuEntry(),
                new CommandMenuEntry("Create remote transaction", new TransferToRemoteBankCommand(), ServiceType.Own),
                new CommandMenuEntry("Create remote withdrawl", new WithdrawalFromRemoteBankCommand(),ServiceType.Own),
                new CommandMenuEntry("View remote transactions", new ViewRemoteTransactionsCommand(),ServiceType.Own),
                new CommandMenuEntry("Remove remote transaction", new RemoveRemoteTransactionCommand(),ServiceType.Own),
                new DividerMenuEntry(),
                new CommandMenuEntry("Exit", new ExitCommand(),ServiceType.All)
            };






      while (true)
      {
        Console.Clear();
        Console.WriteLine("Select action: ");

        menuEntries.Where(x=>x.IsAvailable(selectedService)).ToList().ForEach(x => Console.WriteLine(x.Title));

        var action = Console.ReadLine();
        var selectedEntry = menuEntries.Find(x => x.CanHandle(action));
        if (selectedEntry == null)
        {
          Console.WriteLine("Unknown action.");
          Console.WriteLine("Press key to continue...");
          Console.ReadKey();
          continue;
        }
        try
        {
          selectedEntry.Command.Execute();
        }
        catch (ComponentException e)
        {
          Console.WriteLine($"Command '{selectedEntry.Title}' resulted in an error: '{e.Message}'");
        }
        catch (ExitException)
        {
          Environment.Exit(0);
        }
        catch (Exception e)
        {
          Console.WriteLine($"Unexpected Error occured: Message: {e.Message}");
        }
        Console.WriteLine("Press key to continue...");
        Console.ReadKey();
      }
    }

    private static void ConfigureLogging()
    {
      // Step 1. Create configuration object 
      var config = new LoggingConfiguration();

      // Step 2. Create targets and add them to the configuration 
      var fileTarget = new FileTarget();
      fileTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";
      fileTarget.FileName = "${basedir}\\logfile.log";
      config.AddTarget("file", fileTarget);
      var rule1 = new LoggingRule("*", LogLevel.Trace, fileTarget);
      config.LoggingRules.Add(rule1);
      LogManager.Configuration = config;
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Console.WriteLine($"A unexpected Error occured: {e.ExceptionObject}.");
      Console.WriteLine("Application is Terminating...");
      Console.ReadKey();
      Environment.Exit(-1);
    }

    static ServiceType AskForServiceType()
    {
      while (true)
      {
        Console.WriteLine("What Services do you want to load? 1=foreign 2=own?");
        var answer = Console.ReadLine();

        switch (answer)
        {
          case "1":
            return ServiceType.Foreign;
          case "2":
            return ServiceType.Own;
          default:
            continue;
        }
      }
    }
  }
}
