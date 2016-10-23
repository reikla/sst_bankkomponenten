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

            var menuEntries = new List<MenuEntry>
            {
                new MenuEntry("Create Customer", new CreateCustomerCommand()),



                //Macht sinn dass das der letzte Eintrag ist
                new MenuEntry("\nExit", new ExitCommand())
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
                    Console.WriteLine($"Command '{selectedEntry.Title}' resulted in an error: {e.Message}");
                }
                catch (ExitException e)
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
