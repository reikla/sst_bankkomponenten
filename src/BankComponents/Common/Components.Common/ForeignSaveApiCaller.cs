

using Components.Common.Exceptions;
using System;

namespace Components.Common
{
    public static class ForeignSaveApiCaller
    {
        public static int ExecuteCall(Func<int> apiCall)
        {
            int returnVal;
            try
            {
                returnVal = apiCall();
                if (returnVal == 1)
                {
                    return returnVal;
                }
            }
            catch (Exception e)
            {
                throw new ComponentException("Unexpected Error", e);
            }
            throw ForeignExceptionFactory.GetExceptionByErrorCode(returnVal);
        }
    }
}
