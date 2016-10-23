using System;

namespace Components.Common.Exceptions
{
    public class CustomerException : ComponentException
    {
        public CustomerException()
        {
            
        }

        public CustomerException(string message) : base(message)
        {
            
        }
        public CustomerException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}