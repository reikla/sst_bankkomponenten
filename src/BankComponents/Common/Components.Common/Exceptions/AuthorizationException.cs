using System;

namespace Components.Common.Exceptions
{
    public class AuthorizationException : ComponentException
    {
        public AuthorizationException()
        {
            
        }

        public AuthorizationException(string message) : base(message)
        {
            
        }
        public AuthorizationException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}