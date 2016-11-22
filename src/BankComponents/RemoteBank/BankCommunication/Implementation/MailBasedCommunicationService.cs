using BankCommunication.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace BankCommunication.Implementation
{
    internal class MailBasedCommunicationService : IBankCommunicationService, IDisposable
    {
        private readonly SmtpCredentials smtpCredentials_;
        private readonly ImapCredentials imapCredentials_;

        private ConcurrentBag<RawMessage> currentMessages_;

        private Thread pollingThread_;

        private object threadLock_ = new object();

        public event MessagesAvailable MessagesAvailable;

        public MailBasedCommunicationService(SmtpCredentials smtpCredentials, ImapCredentials imapCredentials)
        {
            smtpCredentials_ = smtpCredentials;
            imapCredentials_ = imapCredentials;
            currentMessages_ = new ConcurrentBag<RawMessage>();
        }

        public Task Acknowledge(RawMessage toAcknowledge)
        {
            var t = new Task(() => {
                lock (threadLock_)
                {
                    ImapConnection imapConnection = ImapConnection.GetInstance(imapCredentials_);
                    imapConnection.Connect();

                    imapConnection.Acknowledge(toAcknowledge);

                    currentMessages_ = new ConcurrentBag<RawMessage>(currentMessages_.Where(m => m.Uid != toAcknowledge.Uid));
                }
            });
            t.Start();
            return t;
        }

        public Task<List<RawMessage>> Receive()
        {
            var t = new Task<List<RawMessage>>(() => {
                return currentMessages_.ToList();
            });
            t.Start();
            return t;
        }

        public Task Send(string from, string recipient, string message)
        {
            // Prepare mail message
            MailMessage mail = new MailMessage(from, recipient, "Bank transmission " + DateTime.Now.Ticks, message);

            // Send mail message
            var client = new SmtpClient();
            client.Host = smtpCredentials_.Url;
            client.Port = smtpCredentials_.Port;            
            client.EnableSsl = smtpCredentials_.UseSecureConnection;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = !smtpCredentials_.UseAuthentication;
            if(smtpCredentials_.UseAuthentication)
            {
                client.Credentials = new System.Net.NetworkCredential(smtpCredentials_.UserName, smtpCredentials_.Password);
            }

            // Start mail send
            return client.SendMailAsync(mail);
        }

        public void StartBackgroundPolling()
        {
            pollingThread_ = new Thread(RunBackgroundPolling);
            pollingThread_.Start();         
        }

        private void RunBackgroundPolling()
        {                        
            while (true)
            {
                lock (threadLock_)
                {
                    // Reconnect to imap
                    ImapConnection imapConnection = ImapConnection.GetInstance(imapCredentials_);
                    imapConnection.Connect();

                    // Poll imap
                    List<RawMessage> messages = imapConnection.GetUnreadRawBankMessages();
                    if (messages.Any())
                    {
                        // Get messages that are not in the current messages
                        bool newMessagesReceived = false;
                        foreach (var message in messages)
                        {
                            if (!currentMessages_.Any(m => m.Uid == message.Uid))
                            {
                                currentMessages_.Add(message);
                                newMessagesReceived = true;
                            }
                        }

                        // Only signal messages available if messages were received
                        if (newMessagesReceived)
                        {
                            MessagesAvailable(this, new EventArgs());
                        }
                    }
                }
                // if any unread mail exist, signal MessageReceived
                Thread.Sleep(10 * 1000); // poll every 5 seconds
            }
        }

        public void Dispose()
        {
            pollingThread_?.Abort();
        }
    }
}
