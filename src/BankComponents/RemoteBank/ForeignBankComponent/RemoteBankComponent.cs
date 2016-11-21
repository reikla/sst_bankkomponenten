using System;
using System.Collections.Generic;
using System.Linq;
using BankCommunication;
using BankMessage;
using NLog;

namespace ForeignBankComponent
{
    public class RemoteBankComponent : IRemoteBankComponent
    {

        private Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IBankCommunicationService _communicationService;
        private readonly string _email;
        private Random rand;
        private readonly SentMessageSerializer _serializer;

        private readonly Dictionary<long, BankMessage.Message> _sentMessages;

        public RemoteBankComponent(string email, string password)
        {

            _email = email;
            var smtpCredentials = new SmtpCredentials
            {
                UserName = email,
                Password = password,
                Port = 587,
                Url = "smtp.gmail.com",
                UseAuthentication = true,
                UseSecureConnection = true
            };

            var imapCredentials = new ImapCredentials
            {
                Login = email,
                Password = password,
                ServerUrl = "imap.gmail.com",
                Port = 993,
                UseSsl = true,
                ValidateServerCertificate = true
            };

            try
            {
                _communicationService = BankCommunicationServiceFactory.GetMailBasedCommunicationService(
                    smtpCredentials,
                    imapCredentials);
            }
            catch (Exception e)
            {
                throw new BankCommunicationException("Cannot communicate.", e);
            }
            _serializer = new SentMessageSerializer();
            _sentMessages = _serializer.Load();

            _communicationService.MessagesAvailable += communicationService_MessagesAvailable;

            rand = new Random();
        }

        private async void communicationService_MessagesAvailable(object sender, EventArgs e)
        {
            var messages = await _communicationService.Receive();

            //Send NACKs back to Bank if a message already expired
            CheckIfMessagesAlreadyExpired();

            _logger.Info($"'{messages.Count}' Messages available.");
            foreach (var rawMessage in messages)
            {
                //commit to not receive message twice
                await _communicationService.Acknowledge(rawMessage);
                _logger.Info($"Committing message '{rawMessage.Uid}'.");
                try
                {
                    var bankMessage = BankMessageParser.BankMessageParser.Deserialize(rawMessage.Payload);
                    if (bankMessage.Ablaufdatum < DateTimeOffset.Now && !(bankMessage.TransaktionsTyp == TransactionType.ACK || bankMessage.TransaktionsTyp == TransactionType.NACK))
                    {
                        _logger.Info($"Message timeout reached. Id: '{bankMessage.MessageID}'");
                        //Timeout reached
                        bankMessage.TransaktionsTyp = TransactionType.NACK;
                        SwitchReceipients(bankMessage);
                        SendTransaction(bankMessage);
                    }else if (bankMessage.TransaktionsTyp == TransactionType.ACK ||
                              bankMessage.TransaktionsTyp == TransactionType.NACK)
                    {
                        if (_sentMessages.ContainsKey(bankMessage.MessageID))
                        {
                            //we received an expecetd ACK/NACK 
                            _logger.Info($"Expected ACK/NACK for message '{bankMessage.MessageID}' received. Removing message from list. Fire MessageReceived event.");
                            _sentMessages.Remove(bankMessage.MessageID);

                            MessageReceived?.Invoke(this, bankMessage);
                        }
                        else
                        {
                            //If message Id not stored we don't expect ACK/NACK -> we throw away.
                            _logger.Info($"Unexpected ACK for Message '{bankMessage.MessageID}' received. Not fire event -> trow away.");
                        }
                    }
                    else
                    {
                        _logger.Info($"Received transaction Id:'{bankMessage.MessageID}'. Transactiontype: '{bankMessage.TransaktionsTyp}'");
                        MessageReceived?.Invoke(this, bankMessage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Received unparsable Email. Spam?");
                    _logger.Error(ex);
                    //probably another email. spam?
                }
            }
        }

        public void SwitchReceipients(BankMessage.Message message)
        {
            var oldTo = message.EmpfaengerBankId;
            var oldFrom = message.AbsenderBankId;
            message.EmpfaengerBankId = oldFrom;
            message.AbsenderBankId = oldTo;
        }

        private void CheckIfMessagesAlreadyExpired()
        {
            var expired = _sentMessages.Values.Where(x => x.Ablaufdatum < DateTimeOffset.Now).ToArray();

            foreach (BankMessage.Message message in expired)
            {
                _logger.Info($"Message timeout reached. Id: '{message.MessageID}'");
                _logger.Info($"Send NACK to Bank: '{message.AbsenderBankId}'");
                message.TransaktionsTyp = TransactionType.NACK;
                message.Ablaufdatum = DateTimeOffset.Now.AddHours(1);
                SwitchReceipients(message);
                MessageReceived?.Invoke(this, message);
                _sentMessages.Remove(message.MessageID);
            }
        }

        public async void SendTransaction(BankMessage.Message message)
        {
            _logger.Info($"Sending Message. Type: '{message.TransaktionsTyp}' Id: '{message.MessageID}' From: '{message.AbsenderBankId}' To: '{message.EmpfaengerBankId}'");

            if (message.TransaktionsTyp == TransactionType.Abbuchung ||
                message.TransaktionsTyp == TransactionType.Ueberweisung)
            {
                if (_sentMessages.ContainsKey(message.MessageID))
                {
                    _logger.Error($"Message with id {message.MessageID} already in list!");
                    throw new BankCommunicationException("Message with same Id already Sent!", null);
                }
                //we expect an ACK/NACK for this message so we store it.
                _sentMessages.Add(message.MessageID, message);
                _serializer.Store(_sentMessages);
            }
            var messageString = BankMessageParser.BankMessageParser.Serialize(message);
            await _communicationService.Send(_email, message.EmpfaengerBankId, messageString);
        }


        public long GetRandomMessageID()
        {
            return LongRandom(1, long.MaxValue, rand);
        }


        private long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public event EventHandler<BankMessage.Message> MessageReceived;
    }
}
