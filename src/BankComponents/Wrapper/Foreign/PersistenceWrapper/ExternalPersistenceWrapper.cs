using Components.Common;

namespace Components.Wrapper.Foreign
{
    public class ExternalPersistenceWrapper
    {
        ///<summary>
        /// As the foreign-dll takes care about the persistence of the data during an action
        /// this method just informs that it is already stored via the peristence layer.
        ///</summary>
        public static void Load()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.IMPLICIT_PERSISTENCE);
        }

        ///<summary>
        /// As the foreign-dll takes care about the persistence of the data during an action
        /// this method just informs that it is already stored via the peristence layer.
        ///</summary>
        public static void Store()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.IMPLICIT_PERSISTENCE);
        }
    }
}
