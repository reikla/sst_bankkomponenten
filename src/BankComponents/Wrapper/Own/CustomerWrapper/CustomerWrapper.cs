

using Components.Common;

using cw = Components.Wrapper.Own.InternalCustomerWrapper;
namespace Components.Wrapper.Own
{
    public static class CustomerWrapper
    {
        public static int CreateCustomer(string firstName, string lastName, string street, int zip)
        {
            int customerId = 0;
            SaveApiCaller.ExecuteCall(
                () => cw.CreateCustomer(firstName, lastName, street, zip, out customerId));
            return customerId;
        }

        public static void DeleteCustomer(int id)
        {
            SaveApiCaller.ExecuteCall(() => cw.DeleteCustomer(id));
        }

        public static void ModifyCustomer(int id, string firstName, string lastName, string street, ref int zip)
        {
            int thezip = zip;
            SaveApiCaller.ExecuteCall(() => cw.ModifyCustomer(id,firstName,lastName,street, ref thezip));
        }
    }
}
