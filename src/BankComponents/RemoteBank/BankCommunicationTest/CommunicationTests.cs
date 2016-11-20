using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankCommunication;
using System.Collections.Generic;
using BankMessage;

namespace BankCommunicationTest
{
    [TestClass]
    public class CommunicationTests
    {
        [TestMethod]
        public void Send_Success()
        {
            // Prepare credentials
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

            IBankCommunicationService mailService = BankCommunicationServiceFactory.GetMailBasedCommunicationService(smtp, imap);

            mailService.Send("bank.ziegler@gmail.com", "bank.ziegler@gmail.com", "Testpayload!").Wait();

            // Should be reached without exception
            Assert.IsTrue(true);
        }
    }
}
