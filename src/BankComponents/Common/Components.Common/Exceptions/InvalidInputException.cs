using System;

namespace Components.Common.Exceptions
{
    public class InvalidInputException : ComponentException
    {
        public InvalidInputException()
        {
            
        }

        public InvalidInputException(string message) : base(message)
        {
            
        }
        public InvalidInputException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}