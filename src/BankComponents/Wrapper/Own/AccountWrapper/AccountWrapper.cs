using Components.Common;

using aw = Components.Wrapper.Own.InternalAccountWrapper;

namespace Components.Wrapper.Own
{
    public static class AccountWrapper
    {
        public static int CreateAccount(int disposerId, string accountName, AccountType type)
        {
            int accountNumber = 0;
            SaveApiCaller.ExecuteCall(() => aw.CreateAccount(disposerId, accountName, type, out accountNumber));
            return accountNumber;
        }

        public static void CloseAccount(int disposerId, int accountNumber)
        {
            SaveApiCaller.ExecuteCall(() => aw.CloseAccount(disposerId, accountNumber));
        }

        public static void AddDisposer(int disposerId, int accountNumber, int newDisposerId)
        {
            SaveApiCaller.ExecuteCall(() => aw.AddDisposer(disposerId, accountNumber, newDisposerId));
        }

        public static void RemoveDisposer(int disposerId, int accountNumber, int disposerToRemove)
        {
            SaveApiCaller.ExecuteCall(() => aw.RemoveDisposer(disposerId, accountNumber, disposerToRemove));
        }


    }
}
