using System;

namespace ForeignBankComponent
{
    public class BankCommunicationException : Exception
    {
        public BankCommunicationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}