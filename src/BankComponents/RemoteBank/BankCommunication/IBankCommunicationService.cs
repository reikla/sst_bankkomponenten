using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankCommunication
{
    /// <summary>
    /// NewMessageReceived is an event that will be fired if there are any new messages to receive
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void MessagesAvailable(object sender, EventArgs e);

    public interface IBankCommunicationService
    {
        /// <summary>
        /// Event will be fired, if polling results in new messages
        /// </summary>
        event MessagesAvailable MessagesAvailable;

        /// <summary>
        /// Send a new message to the recipient address
        /// </summary>
        /// <param name="from">Sender address (e.g. an e-mail address)</param>
        /// <param name="recipient">Recipient address (e.g. an e-mail address)</param>
        /// <param name="message">Message payload to send</param>
        /// <returns></returns>
        Task Send(string from, string recipient, string message);

        /// <summary>
        /// Receive all messages that were not acknowledged yet
        /// </summary>
        /// <returns></returns>
        Task<List<RawMessage>> Receive();

        /// <summary>
        /// Acknowledge message, so that it will not be in the next receive result.
        /// </summary>
        /// <param name="toAcknowledge">Raw message that should be acknowledged with the UID set.</param>
        /// <returns></returns>
        Task Acknowledge(RawMessage toAcknowledge);
    }
}
