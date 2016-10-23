using System;
using System.ComponentModel.Composition;
using Components.Common;
using Components.Contracts.Services;
using Components.Wrapper.Own;

namespace Components.Service.Own
{
    [Export(typeof(ICurrencyTranslationService))]
    public class CurrencyTranslationService : ICurrencyTranslationService
    {
        public void SetCurrencyToEuroFactor()
        {
            var currency = InputParser.GetCurrency();
            var factor = InputParser.GetDoubleInput("Enter factor: ", "Factor", d => d > 0.0);
            CurrencyTranslationWrapper.SetCurrencyToEuroFactor(currency, factor);
            Console.WriteLine("Currency to EUR factor successfully set.");
        }

        public void GetCurrencyToEuroFactor()
        {
            var currency = InputParser.GetCurrency();
            var factor = CurrencyTranslationWrapper.GetCurrencyToEuroFactor(currency);
            Console.WriteLine($"{currency} to Euro factor is {factor}.");
        }

        public void TranslateToEuro()
        {
            var currency = InputParser.GetCurrency();
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var result = CurrencyTranslationWrapper.TranslateToEuro(currency, amount);
            Console.WriteLine($"{amount} {currency} are {result} EUR.");
        }

        public void TranslateFromEuro()
        {
            var currency = InputParser.GetCurrency();
            var amount = InputParser.GetDoubleInput("Enter amount: ", "Amount");
            var result = CurrencyTranslationWrapper.TranslateFromEuro(currency, amount);
            Console.WriteLine($"{amount} EUR are {result} {currency}.");
        }
    }
}
