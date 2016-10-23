using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class TranslateToEuroCommand : ICommand
    {
        public void Execute()
        {
            var translationService = ServiceLocator.Instance().GetService<ICurrencyTranslationService>();
            translationService.TranslateToEuro();
        }
    }
}
