using Components.Common;

namespace Components.Wrapper.Own
{
    public class Transaction
    {
        public static Transaction FromStruct(TransactionStruct s)
        {
            return new Transaction(s.amount, s.factor, s.currency, s.fromAccount, s.toAccount, s.disposer);
        }

        private Transaction(double amount, double factor, OwnCurrency currency, int fromAccount, int toAccount, int disposer)
        {
            _amount = amount;
            _factor = factor;
            _currency = currency;
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _disposer = disposer;
        }

        private readonly double _amount;
        private readonly double _factor;
        private readonly OwnCurrency _currency;
        private readonly int _fromAccount;
        private readonly int _toAccount;
        private readonly int _disposer;

        public double Amount => _amount;
        public double Factor => _factor;
        public OwnCurrency Currency => _currency;
        public int FromAccountNumber => _fromAccount;
        public int ToAccountNumber => _toAccount;
        public int DisposerId => _disposer;

        public TransactionType TransactionType
        {
            get
            {
                if(FromAccountNumber != -1 && ToAccountNumber != -1)
                    return TransactionType.Transaction;
                return FromAccountNumber != -1 ? TransactionType.BarPayOut : TransactionType.BarPayIn;
            }
        }
    }

    public enum TransactionType
    {
        Transaction,
        BarPayIn,
        BarPayOut
    }
}