using System;

namespace Components.Common.Exceptions
{
    public class ComponentException : Exception
    {
        public ComponentException()
        {

        }


        public ComponentException(string message) : base(message)
        {

        }
        public ComponentException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
