using ImapX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankCommunication.Utility
{
    internal class ImapConnection : IDisposable
    {
        protected readonly ImapCredentials credentials_;
        protected ImapClient client_;

        static Dictionary<string, ImapConnection> connectionPool_ = new Dictionary<string, ImapConnection>();

        public static ImapConnection GetInstance(ImapCredentials credentials)
        {
            if(connectionPool_.ContainsKey(credentials.Login))
            {
                // If connection already initialized, return connection from pool
                return connectionPool_[credentials.Login];
            }
            // Else create new connection and add it to the connection pool
            var connection = new ImapConnection(credentials);
            connectionPool_.Add(credentials.Login, connection);

            return connection;
        }

        private ImapConnection(ImapCredentials credentials)
        {
            credentials_ = credentials;
        }

        public void Connect()
        {
            if(client_ != null && client_.IsConnected && client_.IsAuthenticated)
            {
                // Client already initialized
                return;
            }

            try
            {
                client_ = new ImapClient(credentials_.ServerUrl, credentials_.UseSsl, credentials_.ValidateServerCertificate);

                // Connect the client
                if(client_.Connect())
                {
                    Login();
                }
                else
                {
                    throw new ArgumentException("Cannot connect to the given IMAP credentials for the Login: " + credentials_.Login);
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public void Dispose()
        {
            if (client_?.IsAuthenticated ?? false)
            {
                client_.Logout();
            }

            if (client_?.IsConnected ?? false)
            {
                client_.Disconnect();
            }

            client_.Dispose();
        }

        public List<RawMessage> GetUnreadRawBankMessages()
        {
            var results = new List<RawMessage>();

            // Get messages
            var messages = client_.Folders.Inbox.Search();

            foreach(var message in messages)
            {
                if(!message.Seen)
                {
                    results.Add(new RawMessage
                    {
                        Uid = message.UId,
                        Payload = message.Body.Text
                    });
                }
            }

            return results;
        }

        public void Acknowledge(RawMessage message)
        {
            var toEdit = client_.Folders.Inbox.Search(new long[] { message.Uid });
            if(toEdit.Any())
            {
                toEdit[0].Seen = true;
            }
        }

        protected void Login()
        {
            try
            {
                if(!client_.Login(credentials_.Login, credentials_.Password))
                {
                    throw new ArgumentException("Cannot login to the given IMAP credentials. Invalid loginname or password for login: " + credentials_.Login);
                }
                // Else login successful
                client_.Behavior.MessageFetchMode = ImapX.Enums.MessageFetchMode.Full;
                client_.Behavior.AutoPopulateFolderMessages = true;
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}
