using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class SetCurrencyToEuroFactorCommand : ICommand
    {
        public void Execute()
        {
            var translationService = ServiceLocator.Instance().GetService<ICurrencyTranslationService>();
            translationService.SetCurrencyToEuroFactor();

        }
    }
}
