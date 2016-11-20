using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCommunication
{
    /// <summary>
    /// SMTP credentials used for sending messages over the simple mail transfer protocol
    /// </summary>
    public class SmtpCredentials
    {
        /// <summary>
        /// URL of the SMTP Server
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Signal messaging to user authentication
        /// If true, username and password must be set
        /// </summary>
        public bool UseAuthentication { get; set; }

        /// <summary>
        /// If true, SSL will be used for authentication
        /// </summary>
        public bool UseSecureConnection { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// TCP Port for sending
        /// </summary>
        public int Port { get; set; }
    }
}
