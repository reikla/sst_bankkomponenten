using Components.Common;

using tw = Components.Wrapper.Own.InternalCurrencyTranslationWrapper;

namespace Components.Wrapper.Own
{
    public static class CurrencyTranslationWrapper
    {
        public static void SetCurrencyToEuroFactor(OwnCurrency currency, double factor)
        {
            SaveApiCaller.ExecuteCall(() => tw.SetCurrencyToEuroFactor(currency, factor));
        }

        public static double GetCurrencyToEuroFactor(OwnCurrency currency)
        {
            double factor = 0;
            SaveApiCaller.ExecuteCall(() => tw.GetCurrencyToEuroFactor(currency, out factor));
            return factor;
        }

        public static double TranslateToEuro(OwnCurrency currency, double amount)
        {
            double result = 0;
            SaveApiCaller.ExecuteCall(() => tw.TranslateToEuro(currency, amount, out result));
            return result;
        }

        public static double TranslateFromEuro(OwnCurrency currency, double amount)
        {
            double result = 0;
            SaveApiCaller.ExecuteCall(() => tw.TranslateFromEuro(currency, amount, out result));
            return result;
        }
    }
}
