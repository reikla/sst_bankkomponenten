using System.Runtime.InteropServices;
using Components.Common;

namespace Components.Wrapper.Own
{
    internal static class InternalCurrencyTranslationWrapper
    {
        [DllImport(DllNames.OwnCurrencyTranslationModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetCurrencyToEuroFactor(OwnCurrency ownCurrency, double factor);

        [DllImport(DllNames.OwnCurrencyTranslationModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetCurrencyToEuroFactor(OwnCurrency ownCurrency, out double factor);

        [DllImport(DllNames.OwnCurrencyTranslationModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TranslateToEuro(OwnCurrency ownCurrency, double amount, out double result);

        [DllImport(DllNames.OwnCurrencyTranslationModuleName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int TranslateFromEuro(OwnCurrency ownCurrency, double amount, out double result);
    }
}
