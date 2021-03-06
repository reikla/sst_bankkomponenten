﻿using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public void Execute()
        {
            var customerService = ServiceLocator.Instance().GetService<ICustomerService>();
            customerService.CreateCustomer();
        }
    }
}
