using BankCommunication;
using System;
using System.Collections.Generic;

namespace BankCommunicationTestConsole
{
    class Program
    {
        static IBankCommunicationService mailService;

        static void Main(string[] args)
        {
            var smtp = new SmtpCredentials
            {
                UserName = "bank.ziegler@gmail.com",
                Password = "yla4DIQBKL2uZ9sFydND",
                Port = 587,
                Url = "smtp.gmail.com",
                UseAuthentication = true,
                UseSecureConnection = true
            };

            var imap = new ImapCredentials
            {
                Login = "bank.ziegler@gmail.com",
                Password = "yla4DIQBKL2uZ9sFydND",
                ServerUrl = "imap.gmail.com",
                Port = 993,
                UseSsl = true,
                ValidateServerCertificate = true
            };

            mailService = BankCommunicationServiceFactory.GetMailBasedCommunicationService(smtp, imap);
            mailService.MessagesAvailable += MailService_MessagesAvailable;

            Console.WriteLine("Waiting for new messages...");

            while(true)
            {
                // Nothing to do here...
            }
        }

        private static async void MailService_MessagesAvailable(object sender, EventArgs e)
        {
            Console.WriteLine("Messages received! query imap server...");

            // Get messages
            List<RawMessage> messages = await mailService.Receive();

            Console.WriteLine($"{messages.Count} messages received:");

            foreach(var message in messages)
            {
                Console.WriteLine($"{message.Uid}\t\t{message.Payload}");
            }

            Console.WriteLine("Execute Acknowledge:");

            foreach(var message in messages)
            {
                Console.WriteLine($"ACK {message.Uid}");
                await mailService.Acknowledge(message);
            }

            Console.WriteLine("Done Receiving messages");
        }
    }
}
