using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class ModifyCustomerCommand : ICommand
    {
        public void Execute()
        {
            var customerService = ServiceLocator.Instance().GetService<ICustomerService>();
            customerService.ModifyCustomer();
        }
    }
}