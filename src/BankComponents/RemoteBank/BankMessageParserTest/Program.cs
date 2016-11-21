namespace BankMessageParserTest
{
    using System;

    using BankMessage;
    using BankMessageParser;

    class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message(1, 100.2, DateTimeOffset.UtcNow.AddHours(8), "foo@bar.at", "fnord@xy.at", "EUR", "123", "678", TransactionType.Abbuchung, "ABC", 125);

            string serialized = BankMessageParser.Serialize(message);
            Message messageDeserialized = BankMessageParser.Deserialize(serialized);

            Console.WriteLine("Objekt hat den gleichen Zustand wie vor der Serialisierung: " + Message.BankMessageComparer.Equals(message, messageDeserialized));

            try
            {
                Message messageDeserializedWrongFormat = BankMessageParser.Deserialize(serialized.Remove(0, 6));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception mit falschen Format: " + ex.Message);
            }
            

        }
    }
}
