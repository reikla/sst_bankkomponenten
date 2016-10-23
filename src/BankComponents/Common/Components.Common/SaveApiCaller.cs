using System;
using Components.Common.Exceptions;

namespace Components.Common
{
    /// <summary>
    /// This class is used to wrap API calls in a try catch block and throw meaningfull exceptions.
    /// </summary>
    public static class SaveApiCaller
    {
        public static int ExecuteCall(Func<int> apiCall)
        {
            int success;
            try
            {
                success = apiCall();
                if (success >= 0) // Für AccountStatement -> Dort wird die Anzahl an bytes zurück gegeben
                {
                    return success;
                }
            }
            catch (Exception e)
            {
                throw new ComponentException("Unexpected Error", e);
            }

            throw ExceptionFactory.GetExceptionByErrorCode(success);
        }
    }
}