using System;
using System.ComponentModel.Composition;
using Components.Contracts.Services;

namespace Components.Service.Foreign
{
    [Export(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        public void CreateAccount()
        {
            throw new NotImplementedException();
        }

        public void CloseAccount()
        {
            throw new NotImplementedException();
        }

        public void AddDisposer()
        {
            throw new NotImplementedException();
        }

        public void RemoveDisposer()
        {
            throw new NotImplementedException();
        }
    }
}
