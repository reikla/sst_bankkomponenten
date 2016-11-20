using System;

namespace ForeignComponent
{
    public interface IForeignBankComponent
    {
        void SendTransaction(BankMessage.BankMessage message);
        event EventHandler<BankMessage.BankMessage> MessageReceived;
    }
}