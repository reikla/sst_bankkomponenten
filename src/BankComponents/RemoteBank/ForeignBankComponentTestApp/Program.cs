using System;
using System.Linq;
using System.Threading;
using ForeignComponent;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ForeignBankComponentTestApp
{
    class Program
    {
        static void Main(string[] args)
        {


            ConfigLogging();

            var logger = LogManager.GetCurrentClassLogger();

            foreach (var s in args)
            {
                Console.WriteLine($"Argument {s}");
            }

            string email;
            string password;
            string toEmail;

            if (args.Any())
            {
                email = "bank1.reimar@gmail.com";
                password = "EXouaPiL";
                toEmail = "bank.reimar@gmail.com";
                toEmail = "bankssd0@gmail.com";
            }
            else
            {
                email = "bank.reimar@gmail.com";
                password = "pVXbeaYs";
                toEmail = "bank1.reimar@gmail.com";
            }

            logger.Info($"Email: '{email}'");
            logger.Info($"Password: '{password}'");
            logger.Info($"toEmail: '{toEmail}'");










            var foreignBankComponent = new ForeignBankComponent(email, password);
            foreignBankComponent.MessageReceived += ForeignBankComponent_MessageReceived;

            foreignBankComponent.SendTransaction(new BankMessage.BankMessage(1, 1.7, DateTimeOffset.Now.AddHours(8), email, toEmail, "EUR", "0", //new DateTimeOffset(2016,12,1,0,0,0,TimeSpan.Zero)
                "1", BankMessage.TransactionType.Ueberweisung, "Blubb", foreignBankComponent.GetRandomMessageID()));




            while (true)
            {
                logger.Info("Waiting for message...");
                Thread.Sleep(1000);
            }
        }

        private static void ConfigLogging()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";
            var rule1 = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(rule1);
            LogManager.Configuration = config;
        }

        private static void ForeignBankComponent_MessageReceived(object sender, BankMessage.BankMessage e)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info($"Received Message from: '{e.AbsenderBankId}' to '{e.EmpfaengerBankId}'.");
        }
    }
}
