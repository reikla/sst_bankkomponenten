using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using BankingApplication.Commands;
using BankingApplication.Menu;
using Components.Common.Exceptions;

namespace BankingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            AggregateCatalog serviceCatalog = new AggregateCatalog();

            foreach (var assembly in AskForServiceType())
            {
                serviceCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
            }

            CompositionContainer container = new CompositionContainer(serviceCatalog);

            container.ComposeParts();

            ServiceLocator.Instance().Init(container);

            var menuEntries = new List<IMenuEntry>
            {
                new CommandMenuEntry("Create customer", new CreateCustomerCommand()),
                new CommandMenuEntry("Modify customer", new ModifyCustomerCommand()),
                new CommandMenuEntry("Delete customer", new DeleteCustomerCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Create account", new CreateAccountCommand()),
                new CommandMenuEntry("Delete account", new DeleteAccountCommand()),
                new CommandMenuEntry("Add disposer from account", new AddDisposerCommand()),
                new CommandMenuEntry("Remove disposer from account", new RemoveDisposerCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Set currency to Euro factor", new SetCurrencyToEuroFactorCommand()),
                new CommandMenuEntry("Get currency from Euro factor", new GetCurrencyToEuroFactorCommand()),
                new CommandMenuEntry("Translate to Euro", new TranslateToEuroCommand()),
                new CommandMenuEntry("Translate from Euro", new TranslateFromEuroCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Pay out", new PayOutCommand()),
                new CommandMenuEntry("Pay in", new PayInCommand()),
                new CommandMenuEntry("Transfer money", new TransferCommand()),
                new CommandMenuEntry("Account statement", new AccountStatementCommand()),
                new CommandMenuEntry("Account balance", new AccountBalanceCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Load Data", new LoadCommand()),
                new CommandMenuEntry("Save Data", new SaveCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Create remote transaction", new TransferToRemoteBankCommand()),
                new CommandMenuEntry("Create remote withdrawl", new WithdrawalFromRemoteBankCommand()),
                new CommandMenuEntry("View remote transactions", new ViewRemoteTransactionsCommand()),
                new DividerMenuEntry(),
                new CommandMenuEntry("Exit", new ExitCommand())
            };






            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select action: ");

                menuEntries.ForEach(x=>Console.WriteLine(x.Title));

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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"A unexpected Error occured: {e.ExceptionObject}.");
            Console.WriteLine("Application is Terminating...");
            Console.ReadKey();
            Environment.Exit(-1);
        }

        static IEnumerable<Assembly> AskForServiceType()
        {
                while (true)
                {
                    Console.WriteLine("What Services do you want to load? 1=foreign 2=own?");
                    var answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "1":
                            return AssemblyLocator.GetServiceAssemblies(ServiceType.Foreign);
                        case "2":
                            return AssemblyLocator.GetServiceAssemblies(ServiceType.Own);
                        default:
                            continue;
                    }
                }
            }
    }
}
