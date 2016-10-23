using System;

namespace Components.Common.Exceptions
{
    public class TransactionException : ComponentException
    {
        public TransactionException()
        {
            
        }

        public TransactionException(string message) : base(message)
        {
            
        }
        public TransactionException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}