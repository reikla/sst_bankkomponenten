using System;
using BankMessage;

namespace ForeignBankComponent
{
    /// <summary>
    /// Component that encapsulates the Communication between local and remote bank
    /// </summary>
    public interface IRemoteBankComponent
    {
        /// <summary>
        /// Send a Message to a remote bank.
        /// </summary>
        /// <param name="message">The message does specify what kind of message is sent.
        /// the <see cref="Message.TransaktionsTyp"/> specififies this (ACK,NACK,UEBERWEISUNG,ABBUCHUNG)
        /// </param>
        void SendTransaction(BankMessage.Message message);

        /// <summary>
        /// The event is fired when a message is received.
        /// There won't be another notification if a message is received and no one is receiving the event yet.
        /// </summary>
        event EventHandler<BankMessage.Message> MessageReceived;
    }
}