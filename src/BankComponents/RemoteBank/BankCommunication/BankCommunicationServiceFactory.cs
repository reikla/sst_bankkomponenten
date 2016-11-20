using BankCommunication.Implementation;
using BankCommunication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCommunication
{
    public static class BankCommunicationServiceFactory
    {

        public static IBankCommunicationService GetMailBasedCommunicationService(SmtpCredentials smtpCredentials, ImapCredentials imapCredentials)
        {
            var mailBasedService = new MailBasedCommunicationService(smtpCredentials, imapCredentials);
            mailBasedService.StartBackgroundPolling();
            return mailBasedService;
        }
    }
}
