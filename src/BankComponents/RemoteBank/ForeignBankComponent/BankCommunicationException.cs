using System;

namespace ForeignComponent
{
    public class BankCommunicationException : Exception
    {
        public BankCommunicationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}