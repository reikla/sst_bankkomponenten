namespace Components.Contracts.Services
{
    public interface ITransactionService
    {
        void PayOut();
        void PayIn();
        void Transfer();
        void AccountStatement();
        void AccountBalancing();
    }
}
