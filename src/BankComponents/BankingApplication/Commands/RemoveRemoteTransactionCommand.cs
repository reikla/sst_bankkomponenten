using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Contracts.Services;

namespace BankingApplication.Commands
{
    class RemoveRemoteTransactionCommand : ICommand
    {
        public void Execute()
        {
            var service = ServiceLocator.Instance().GetService<IRemoteBankService>();
            service.RemoveOpenRemoteTransactions();
        }
    }
}
