using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Own
{
    [Export(typeof(ITransactionService))]
    public class TransactionService : ITransactionService
    {
    }
}
