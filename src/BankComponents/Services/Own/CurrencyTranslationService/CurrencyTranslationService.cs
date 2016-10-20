using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Own
{
    [Export(typeof(ICurrencyTranslationService))]
    public class CurrencyTranslationService : ICurrencyTranslationService
    {
    }
}
