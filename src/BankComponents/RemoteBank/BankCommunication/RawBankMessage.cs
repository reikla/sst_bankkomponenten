using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
