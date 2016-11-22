using BankCommunication.Implementation;

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
