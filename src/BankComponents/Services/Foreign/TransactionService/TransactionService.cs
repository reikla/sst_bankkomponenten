using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(ITransactionService))]
    public class TransactionService : ITransactionService
    {
    }
}
