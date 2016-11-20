//Sebastian Buchegger
namespace BankMessageParser
{
    using System;
    using BankMessage;

    // Parse string to: 1;222.33;45454445;aaa@aa.at;aab@aa.at;EUR;100;125;0;adasdasdad;555
    //Version;Betrag;Ablaufdatum;AbsenderBankId;EmpfaengerBankId;Waehrung;AbsenderKonto;EmpfaengerKonto;TransaktionsTyp;MessageID;Verwendungszweck 
    //Alles bis auf Verwendungszweck Pflicht = 11 Felder mindestens
    //11 Felder

    public class BankMessageParser
    {
        private static char delimiter = '§';

        public static char Delimiter
        {
            get { return delimiter; }
            set { delimiter = value; }
        }

        public static string Serialize(BankMessage message)
        {
            char d = delimiter;
            return
                $"{message.Version}{d}{message.Betrag}{d}{message.Ablaufdatum.ToUnixTimeSeconds()}{d}{message.AbsenderBankId}{d}{message.EmpfaengerBankId}{d}{message.Waehrung}{d}{message.AbsenderKonto}{d}{message.EmpfaengerKonto}{d}{(int)message.TransaktionsTyp}{d}{message.MessageID}{d}{message.Verwendungszweck}";
        }

        public static BankMessage Deserialize(string serializedText)
        {
            string[] splitted = serializedText.Split(delimiter);

            if (splitted.Length < 11)
            {
                throw new Exception("Falsches Format / Zuwenig Felder");
            }

            int Version;
            if (!int.TryParse(splitted[0], out Version))
            {
                throw new Exception("Error Version ist keine Zahl");
            }

            double Betrag;
            if (!double.TryParse(splitted[1], out Betrag))
            {
                throw new Exception("Error Betrag ist keine Gleitkommazahl");
            }


            DateTimeOffset Ablaufdatum;
            long AblaufdatumL;
            if (!long.TryParse(splitted[2], out AblaufdatumL))
            {
                throw new Exception("Error Ablaufdatum ist keine Zahl");
            }

            Ablaufdatum = DateTimeOffset.FromUnixTimeSeconds(AblaufdatumL);




            TransactionType TransaktionsTyp;

            int TransaktionsTypI;
            if (!int.TryParse(splitted[8], out TransaktionsTypI))
            {
                throw new Exception("Error TransaktionsTyp ist keine Zahl");
            }

            if (Enum.IsDefined(typeof(TransactionType), TransaktionsTypI))
            {
                TransaktionsTyp = (TransactionType)TransaktionsTypI;
            }
            else
            {
                throw new Exception("Typ existiert nicht");
            }

            long MessageID;
            if (!long.TryParse(splitted[9], out MessageID))
            {
                throw new Exception("Error Ablaufdatum ist keine Zahl");
            }

            return new BankMessage() { Version = Version, Betrag = Betrag, Ablaufdatum = Ablaufdatum, AbsenderBankId = splitted[3], EmpfaengerBankId = splitted[4], Waehrung = splitted[5], AbsenderKonto = splitted[6],
                                       EmpfaengerKonto = splitted[7], TransaktionsTyp = TransaktionsTyp, Verwendungszweck = splitted[10], MessageID = MessageID };

        }
    }
}
