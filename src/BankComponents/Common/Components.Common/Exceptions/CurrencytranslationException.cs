using System;

namespace Components.Common.Exceptions
{
    public class CurrencyTranslationException : ComponentException
    {
        public CurrencyTranslationException()
        {
            
        }

        public CurrencyTranslationException(string message) : base(message)
        {
            
        }
        public CurrencyTranslationException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}