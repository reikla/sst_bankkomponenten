using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ITransactionService))]
    public class TransactionService : ITransactionService
    {
        public void PayOut()
        {
            throw new System.NotImplementedException();
        }

        public void PayIn()
        {
            throw new System.NotImplementedException();
        }

        public void Transfer()
        {
            throw new System.NotImplementedException();
        }

        public void AccountStatement()
        {
            throw new System.NotImplementedException();
        }

        public void AccountBalancing()
        {
            throw new System.NotImplementedException();
        }
    }
}
