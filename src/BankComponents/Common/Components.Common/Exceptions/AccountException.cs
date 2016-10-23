using System;

namespace Components.Common.Exceptions
{
    public class AccountException : ComponentException
    {
        public AccountException()
        {
            
        }

        public AccountException(string message) : base(message)
        {
            
        }
        public AccountException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}