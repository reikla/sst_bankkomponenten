namespace Components.Contracts.Services
{
    public interface IAccountService
    {
        void CreateAccount();
        void CloseAccount();
        void AddDisposer();
        void RemoveDisposer();
    }
}