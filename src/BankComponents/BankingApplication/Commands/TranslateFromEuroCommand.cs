using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class TranslateFromEuroCommand : ICommand
    {
        public void Execute()
        {
            var translationService = ServiceLocator.Instance().GetService<ICurrencyTranslationService>();
            translationService.TranslateFromEuro();
        }
    }
}
