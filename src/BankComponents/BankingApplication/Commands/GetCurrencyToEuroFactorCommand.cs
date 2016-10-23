using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class GetCurrencyToEuroFactorCommand : ICommand
    {
        public void Execute()
        {
            var translationService = ServiceLocator.Instance().GetService<ICurrencyTranslationService>();
            translationService.GetCurrencyToEuroFactor();
        }
    }
}
