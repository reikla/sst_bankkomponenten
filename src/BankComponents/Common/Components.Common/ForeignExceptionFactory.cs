using Components.Common.Exceptions;


namespace Components.Common
{
    public static class ForeignExceptionFactory
    {
        public const int ACCOUNT_FEATURE_NOT_SUPPORTED = 1000;
        public const int CUSTOMER_FEATURE_NOT_SUPPORTED = 1001;
        public const int TRANSACTION_FEATURE_NOT_SUPPORTED = 1002;
        public const int CURRENCY_FEATURE_NOT_SUPPORTED = 1003;
        public const int IMPLICIT_PERSISTENCE = 1004;
        public static ComponentException GetExceptionByErrorCode(int errorCode)
        {
            switch (errorCode)
            {
                case -1:
                    return new AccountException("An error occures while calling the persistence layer.");
                case -2:
                    return new AccountException("Invalid customer ID.");
                case -3:
                    return new AccountException("Invalid IBAN - has to be between 15 and 31 characters.");
                case -4:
                    return new AccountException("Invalid BIC - has to be between 8 and 11 characters.");
                case -5:
                    return new AccountException("Invalid account type - either 0 for an Giro Account or 1 for a Savings Account.");
                case -6:
                    return new AccountException("Null value recognized - your account data are not complete.");
                case -100:
                    return new CustomerException("Single character names are to short.");
                case -102:
                    return new CustomerException("A name is not allowed to have more than 35 characters.");
                case -103:
                    return new CustomerException("You have forgotten to set a firstname or lastname.");
                case -104:
                    return new CustomerException("There exists no customer with the given ID.");
                case -105:
                    return new CustomerException("Invalid customer ID.");
                case -106:
                    return new CustomerException("There are still active accoutns.");
                case -150:
                    return new CustomerException("NO connection to the data persistence layer.");
                case -600:
                    return new TransactionException("Amount has to be positive.");
                case -601:
                    return new TransactionException("Target account not found.");
                case -602:
                    return new TransactionException("Source account not found.");
                case -603:
                    return new TransactionException("Source account's overdraft facility is to small for this transaction.");
                case -604:
                    return new TransactionException("Account balance is to small for this transaction.");
                case -605:
                    return new TransactionException("Transaction could not be processed.");
                case -606:
                    return new TransactionException("No connection to the data persistence layer.");
                case ACCOUNT_FEATURE_NOT_SUPPORTED:
                    return new AccountException("Sorry - the current dll is not supporting this account feature.");
                case CUSTOMER_FEATURE_NOT_SUPPORTED:
                    return new AccountException("Sorry - the current dll is not supporting this customer feature.");
                case TRANSACTION_FEATURE_NOT_SUPPORTED:
                    return new AccountException("Sorry - the current dll is not supporting this transaction feature.");
                case CURRENCY_FEATURE_NOT_SUPPORTED:
                    return new CurrencyTranslationException("Sorry - the current dll is not supporting this currency translation feature.");
                case IMPLICIT_PERSISTENCE:
                    return new PersistenceException("Is already done - the current dll persists and loads the data implicitly.");
                default:
                    return new ComponentException("Unexpected Error.");
            }
        }
    }
}
