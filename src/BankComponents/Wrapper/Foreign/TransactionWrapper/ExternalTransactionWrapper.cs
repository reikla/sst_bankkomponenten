using Components.Common;
using System.Collections.Generic;
using tw = Components.Wrapper.Foreign.TransactionWrapper;
namespace Components.Wrapper.Foreign
{
    public static class ExternalTransactionWrapper
    {
        public static void PayOut(int accountId, double amount, ForeignCurrency currency)
        {
            ForeignSaveApiCaller.ExecuteCall(
                () => tw.auszahlung(currency, amount, accountId));
        }

        public static void PayIn(int accountId, double amount, ForeignCurrency currency)
        {
            ForeignSaveApiCaller.ExecuteCall(() => tw.einzahlung(currency, amount, accountId));
        }

        public static void Transfer(ForeignCurrency currency, double amount, int targetAccountId, int sourceAccountId)
        {
            ForeignSaveApiCaller.ExecuteCall(() => tw.ueberweisung(currency, amount, targetAccountId, sourceAccountId));
        }

        /// <summary>
        /// As there exists no equivalent in the foreign dll an exception is thrown automatically.
        /// </summary>
        public static IEnumerable<ForeignTransactionStruct> AccountStatement()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.TRANSACTION_FEATURE_NOT_SUPPORTED);
        }

        public static double AccountBalancing(int accountId, ForeignCurrency currency)
        {
            double balance = -1;
            ForeignSaveApiCaller.ExecuteCall(() => tw.calcSaldo(accountId, currency, out balance));
            return balance;
        }
    }
}
