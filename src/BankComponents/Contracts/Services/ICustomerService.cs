namespace Components.Contracts.Services
{
    public interface ICustomerService
    {
        int CreateCustomer(string firstName, string lastName, string street, int zip);
        void DeleteCustomer(int customerId);
        void ModifyCustomer(int customerId, string firstName, string lastName, string street, int zip);
    }
}