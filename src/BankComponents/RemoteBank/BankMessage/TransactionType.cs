namespace BankMessage
{
    public enum TransactionType : int
    {
        Abbuchung = 0,
        Ueberweisung = 1,
        ACK = 2,
        NACK = 3
    }
}