namespace Components.Contracts.Services
{
    public interface ICurrencyTranslationService
    {
        void SetCurrencyToEuroFactor();
        void GetCurrencyToEuroFactor();
        void TranslateToEuro();
        void TranslateFromEuro();
    }
}