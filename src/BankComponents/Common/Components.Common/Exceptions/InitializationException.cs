using System;

namespace Components.Common.Exceptions
{
    public class InitializationException : ComponentException
    {
        public InitializationException()
        {
            
        }

        public InitializationException(string message) : base(message)
        {
            
        }
        public InitializationException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}