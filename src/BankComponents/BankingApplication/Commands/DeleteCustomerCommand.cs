using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class DeleteCustomerCommand : ICommand
    {
        public void Execute()
        {
            var customerService = ServiceLocator.Instance().GetService<ICustomerService>();
            customerService.DeleteCustomer();
        }
    }
}