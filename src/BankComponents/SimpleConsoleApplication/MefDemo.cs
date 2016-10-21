using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Components.Contracts.Services;

namespace Components.App.MEFDemo
{
    class MefDemo
    {
        static void Main(string[] args)
        {
            var ag = new AggregateCatalog();

            var assemblyPath = AskForAssembly();

            var accountServiceCatalog = new AssemblyCatalog(Assembly.LoadFrom(assemblyPath));

            ag.Catalogs.Add(accountServiceCatalog);

            CompositionContainer c = new CompositionContainer(ag);
            c.ComposeParts();

            var accountService = c.GetExportedValue<IAccountService>();

            accountService.Foo();

            Console.ReadKey();
        }

        static string AskForAssembly()
        {
            while (true)
            {
                Console.WriteLine("What assembly do you want to load? 1=foreign 2=own?");
                var answer = Console.ReadKey();

                switch (answer.KeyChar)
                {
                    case '1':
                        return "Components.Service.Foreign.AccountService.dll";
                    case '2':
                        return "Components.Service.Own.AccountService.dll";
                    default:
                        continue;
                }
            }
        }
    }
}
