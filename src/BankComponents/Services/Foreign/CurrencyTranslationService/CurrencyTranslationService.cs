using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ICurrencyTranslationService))]
    public class CurrencyTranslationService : ICurrencyTranslationService
    {
    }
}
