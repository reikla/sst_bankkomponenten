namespace Components.Contracts.Services
{
    public interface IRemoteBankService
    {
        void Transfer();
        void Withdrawl();
        void ViewRemoteTransactions();
    }
}