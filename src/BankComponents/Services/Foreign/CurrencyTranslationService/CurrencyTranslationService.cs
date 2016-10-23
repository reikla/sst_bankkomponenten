using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ICurrencyTranslationService))]
    public class CurrencyTranslationService : ICurrencyTranslationService
    {
        public void SetCurrencyToEuroFactor()
        {
            throw new System.NotImplementedException();
        }

        public void GetCurrencyToEuroFactor()
        {
            throw new System.NotImplementedException();
        }

        public void TranslateToEuro()
        {
            throw new System.NotImplementedException();
        }

        public void TranslateFromEuro()
        {
            throw new System.NotImplementedException();
        }
    }
}
