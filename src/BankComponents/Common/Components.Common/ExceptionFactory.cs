using Components.Common.Exceptions;

namespace Components.Common
{

    /// <summary>
    /// Factory to generate Exceptions from errorcodes.
    /// </summary>
    public static class ExceptionFactory
    {

        public static ComponentException GetExceptionByErrorCode(int errorCode)
        {
            switch (errorCode)
            {
                case -1:
                    return new ComponentException("Unexpected Error.");
                case -2:
                    return new PersistenceException("Persistence Error.");
                case -10:
                    return new AuthorizationException("Unauthorized.");
                case -20:
                    return new InitializationException("Not Initialized.");
                case -30:
                    return new InvalidParameterException("Invalid parameter.");
                case -41:
                    return new CustomerException("Customer not found");
                case -51:
                    return new AccountException("Account not found.");
                case -52:
                    return new AccountException("New Disposer not found.");
                case -53:
                    return new AccountException("Disposer to remove not found.");
                case -54:
                    return new AccountException("Disposer can not remove self.");
                case -60:
                    return new CurrencyTranslationException("Currency Factor not stored.");
                case -70:
                    return new TransactionException("Insufficient funds.");
                case -71: 
                    return new TransactionException("Target Account not found.");
                    
                default:
                    return new ComponentException("Unexpected Error.");
            }
        }

    }
}

/*
#pragma region OK
#define E_OK 0
#pragma endregion

#pragma region Unexpected
#define E_NOT_EXPECTED -1
#pragma endregion Unexpected

#pragma region Persistence
#define E_PERSISTENCE_ERROR -2
#pragma endregion Persistence

#pragma region Authorization
#define E_UNAUTHORIZED -10
#pragma endregion Unauthorized

#pragma region Initialize
#define E_NOT_INITIALIZED -20;
#pragma endregion Initialize

#pragma	region Parameter
#define E_INVALID_PARAMETER -30
#pragma	endregion Parameter

#pragma region Customer
#define E_CUSTOMER_NOT_FOUND -41
#pragma endregion Customer

#pragma region Account
#define E_ACCOUNT_NOT_FOUND -51
#define E_NEW_DISPOSER_NOT_FOUND -52
#define E_REMOVE_DISPOSER_NOT_FOUND -53
#define E_CANNOT_REMOVE_SELF -54
#pragma endregion Account


#pragma region CurrencyTranslation
#define E_CURRENCY_FACTOR_NOT_STORED -60
#pragma endregion CurrencyTranslation

#pragma region Transaction
#define E_INSUFFICIENT_FUNDS -70
#define E_TARGET_ACCOUNT_NOT_FOUND -71
#pragma endregion Transaction* 
 */
