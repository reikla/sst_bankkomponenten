using System;

namespace Components.Common.Exceptions
{
    public class PersistenceException : ComponentException
    {
        public PersistenceException()
        {
            
        }

        public PersistenceException(string message) : base(message)
        {
            
        }
        public PersistenceException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}