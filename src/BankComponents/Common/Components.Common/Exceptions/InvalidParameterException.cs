using System;

namespace Components.Common.Exceptions
{
    public class InvalidParameterException : ComponentException
    {
        public InvalidParameterException()
        {
            
        }

        public InvalidParameterException(string message) : base(message)
        {
            
        }
        public InvalidParameterException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}