using System.ComponentModel.Composition;
using Components.Contracts.Services;
using Components.Common;

namespace Components.Service.Foreign
{
    [Export(typeof(ICurrencyTranslationService))]
    public class CurrencyTranslationService : ICurrencyTranslationService
    {
        /// <summary>
        /// As the foreign-dll has no other currencies than euro
        /// an exception is thrown instead informing about this issue.
        /// </summary>
        public void SetCurrencyToEuroFactor()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CURRENCY_FEATURE_NOT_SUPPORTED);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// As the foreign-dll has no other currencies than euro
        /// an exception is thrown instead informing about this issue.
        /// </summary>
        public void GetCurrencyToEuroFactor()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CURRENCY_FEATURE_NOT_SUPPORTED);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// As the foreign-dll has no other currencies than euro
        /// an exception is thrown instead informing about this issue.
        /// </summary>
        public void TranslateToEuro()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CURRENCY_FEATURE_NOT_SUPPORTED);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// As the foreign-dll has no other currencies than euro
        /// an exception is thrown instead informing about this issue.
        /// </summary>
        public void TranslateFromEuro()
        {
            throw ForeignExceptionFactory.GetExceptionByErrorCode(ForeignExceptionFactory.CURRENCY_FEATURE_NOT_SUPPORTED);
            //throw new System.NotImplementedException();
        }
    }
}
