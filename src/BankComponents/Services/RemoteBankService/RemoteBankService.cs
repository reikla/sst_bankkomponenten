using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using BankMessage;
using Components.Common;
using Components.Contracts.Services;
using Components.Wrapper.Own;
using ForeignBankComponent;
using NLog;
using TransactionType = BankMessage.TransactionType;

namespace Components.Service.RemoteBankService
{
    [Export(typeof(IRemoteBankService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RemoteBankService : IRemoteBankService
    {
        private RemoteBankComponent _remoteBank;
        private Dictionary<long, Message> _messages;
        private SentMessageSerializer _serializer;
        private readonly Logger _logger;

        public RemoteBankService()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Starting remote Bank Service");
            _remoteBank = new RemoteBankComponent("bank.reimar@gmail.com", "XtiKNNJH");
            _serializer = new SentMessageSerializer("local_messages.xml");
            _messages = _serializer.Load();
            _remoteBank.MessageReceived += RemoteBankOnMessageReceived;
        }



        public void Transfer()
        {
            var localAccountId = InputParser.GetIntInput("Enter accountId: ", "localCustomerId", i => i >= 0);
            var ammount = InputParser.GetDoubleInput("Enter ammount: ", "ammount", d => d > 0);
            var currency = InputParser.GetCurrency();
            var foreignBankId = InputParser.GetStringInput("Enter remote bankId: ", "foreignBankId");
            var foreignAccountId = InputParser.GetStringInput("Enter remote accountId: ", "foreignAccountId");
            TransactionWrapper.PayOut(0, localAccountId, ammount, currency);
            //if we get any problem, we get an exception and no message is sent.
            var message = new Message(1, ammount, DateTimeOffset.Now.AddDays(1), "bank.reimar@gmail.com", foreignBankId, currency.ToString(), $"{localAccountId}", foreignAccountId, TransactionType.Ueberweisung, "Ueberweisung", _remoteBank.GetRandomMessageID());
            _remoteBank.SendTransaction(message);
            AddMessage(message);
        }

        public void Withdrawl()
        {
            var localAccountId = InputParser.GetIntInput("Enter accountId: ", "localCustomerId", i => i >= 0);
            var ammount = InputParser.GetDoubleInput("Enter ammount: ", "ammount", d => d > 0);
            var currency = InputParser.GetCurrency();
            var foreignBankId = InputParser.GetStringInput("Enter remote bankId: ", "foreignBankId");
            var foreignAccountId = InputParser.GetStringInput("Enter remote accountId: ", "foreignAccountId");
            TransactionWrapper.PayIn(0, localAccountId, ammount, currency);
            //if we get any problem, we get an exception and no message is sent.
            var message = new Message(1, ammount, DateTimeOffset.Now.AddDays(1), "bank.reimar@gmail.com", foreignBankId, currency.ToString(), $"{localAccountId}", foreignAccountId, TransactionType.Abbuchung, "Abbuchung", _remoteBank.GetRandomMessageID());
            _remoteBank.SendTransaction(message);
            AddMessage(message);
        }

        public void ViewOpenRemoteTransactions()
        {
            Console.WriteLine("List of remote transactions.");
            foreach (var message in _messages.Values)
            {
                Console.WriteLine(message);
            }
        }

        public void RemoveOpenRemoteTransactions()
        {
            ViewOpenRemoteTransactions();
            var messageId = InputParser.GetLongInput("Enter Id for message to remove: ", "MessageId");
            if (_messages.ContainsKey(messageId))
            {
                _messages.Remove(messageId);
                Console.WriteLine("Message Removed");
                return;
            }
            Console.WriteLine("Invalid Message Id");
        }


        private void ProcessIncomingUeberweisung(Message message)
        {
            try
            {
                var currency = (OwnCurrency)Enum.Parse(typeof(OwnCurrency), message.Waehrung, true);

                TransactionWrapper.PayIn(int.Parse(message.AbsenderKonto), int.Parse(message.EmpfaengerKonto), message.Betrag, currency);
                _logger.Info($"Successfully payed in {message.Betrag} to account '{message.EmpfaengerKonto}'");
                SendAck(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Could not process incoming Ueberweisung");
                SendNack(message);
            }
        }

        private void ProcessIncomingAbbuchung(Message message)
        {
            try
            {
                var currency = (OwnCurrency)Enum.Parse(typeof(OwnCurrency), message.Waehrung, true);

                TransactionWrapper.PayOut(int.Parse(message.AbsenderKonto), int.Parse(message.EmpfaengerKonto),
                    message.Betrag, currency);
                _logger.Info($"Successfully payd out in {message.Betrag} from account '{message.EmpfaengerKonto}'");
                SendAck(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Could not process incoming Abbuchung");
                SendNack(message);
            }
        }

        private void ProcessAckNack(Message message)
        {
            if (!_messages.ContainsKey(message.MessageID))
            {
                //we received an ack/nack were not expecting. log and bye
                _logger.Info($"Received unexpected ACK/NACK. MessageId: '{message.MessageID}'. From: {message.AbsenderBankId}");
                return;
            }
            var sentMsg = _messages[message.MessageID];
            //now we received expected either ACK or a NACK
            if (message.TransaktionsTyp == TransactionType.NACK) //transaction rejected -> we have to undo our transaction and then remove message from list
            {
                _logger.Info($"Received an NACK to our Transaction '{message.MessageID}'. Undoing local transaction.");
                var type = sentMsg.TransaktionsTyp;
                if (type == TransactionType.Ueberweisung)
                {
                    //we have to pay in now. We trust the data because we stored it ourself
                    TransactionWrapper.PayIn(0, int.Parse(sentMsg.AbsenderKonto), sentMsg.Betrag,
                        (OwnCurrency) Enum.Parse(typeof(OwnCurrency), sentMsg.Waehrung));
                }
                else
                {
                    //we have to pay out now. We trust the data because we stored it ourself
                    TransactionWrapper.PayOut(0, int.Parse(sentMsg.AbsenderKonto), sentMsg.Betrag,
                        (OwnCurrency)Enum.Parse(typeof(OwnCurrency), sentMsg.Waehrung));
                }
                RemoveMessage(message);
                return;
            }
            switch (sentMsg.TransaktionsTyp)
            {
                case TransactionType.Abbuchung:
                    ProcessOutgoingAbbuchungAck(message);
                    break;
                case TransactionType.Ueberweisung:
                    ProcessOutgoingUeberweisungAck(message);
                    break;
            }
        }

        private void ProcessOutgoingUeberweisungAck(Message message)
        {
            try
            {
                _logger.Info("Received Ack to a earlier 'Ueberweisung'. Everything ok.");
                RemoveMessage(message);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Could not pay out transaction '{message.MessageID}'");
            }

        }

        private void ProcessOutgoingAbbuchungAck(Message message)
        {
            try
            {
                _logger.Info("Received Ack to a earlier 'Abbuchung'. Everything ok.");
                RemoveMessage(message);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Could not pay out transaction '{message.MessageID}'");
            }
        }

        private static Message SwitchRecipientAndAddTimespan(Message message)
        {
            var oldRecipient = message.EmpfaengerBankId;
            var oldSender = message.AbsenderBankId;
            message.AbsenderBankId = oldRecipient;
            message.EmpfaengerBankId = oldSender;
            message.Ablaufdatum = message.Ablaufdatum.AddHours(8);
            return message;
        }

        private void SendAck(Message message)
        {
            message = SwitchRecipientAndAddTimespan(message);
            message.TransaktionsTyp = TransactionType.ACK;
            _remoteBank.SendTransaction(message);
        }

        private void SendNack(Message message)
        {
            message = SwitchRecipientAndAddTimespan(message);
            message.TransaktionsTyp = TransactionType.NACK;
            _remoteBank.SendTransaction(message);
        }

        private void RemoveMessage(Message message)
        {
            _logger.Info("Removing Message");
            _logger.Info(message);
            _messages.Remove(message.MessageID);
            _serializer.Store(_messages);
        }

        private void AddMessage(Message message)
        {
            _logger.Info("Add Message");
            _logger.Info(message);
            _messages.Add(message.MessageID, message);
            _serializer.Store(_messages);
        }

        private void RemoteBankOnMessageReceived(object sender, Message message)
        {
            try
            {
                switch (message.TransaktionsTyp)
                {
                    case TransactionType.Abbuchung:
                        ProcessIncomingAbbuchung(message);
                        break;
                    case TransactionType.Ueberweisung:
                        ProcessIncomingUeberweisung(message);
                        break;
                    case TransactionType.ACK:
                    case TransactionType.NACK:
                        ProcessAckNack(message);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unexpected Exception occured during receive of message");
                _logger.Error(message);
            }

        }
    }
}