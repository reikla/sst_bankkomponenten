using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCommunication
{
    /// <summary>
    /// IMAP Credentials for the authentication of the imap inboxes to use
    /// </summary>
    public class ImapCredentials : IEqualityComparer<ImapCredentials>
    {
        /// <summary>
        /// The login name for the imap authentication.
        /// In most cases, the email address of the account is used.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password of the email inbox
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Url to the IMAP server.
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// TCP port of the IMAP server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// True, if IMAPS should be used (secure transmission)
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Option, if the ssl certificate should be checked to be valid
        /// </summary>
        public bool ValidateServerCertificate { get; set; }

        public bool Equals(ImapCredentials x, ImapCredentials y)
        {
            return x.Login.Equals(y.Login) &&
                x.Password.Equals(y.Password) &&
                x.ServerUrl.Equals(y.ServerUrl) &&
                x.Port == y.Port &&
                x.UseSsl == y.UseSsl &&
                x.ValidateServerCertificate == y.ValidateServerCertificate;
        }

        public int GetHashCode(ImapCredentials obj)
        {
            return (obj.Login + obj.Password + obj.ServerUrl + Port.ToString() + UseSsl.ToString() + ValidateServerCertificate.ToString()).GetHashCode();
        }
    }
}
