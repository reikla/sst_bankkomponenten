namespace BankCommunication
{
    /// <summary>
    /// Raw bank message
    /// </summary>
    public class RawMessage
    {
        /// <summary>
        /// Unique id of the inbox for the result message
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// Payload of the raw bank Message
        /// </summary>
        public string Payload { get; set; }
    }
}
