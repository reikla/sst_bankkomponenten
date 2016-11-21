//Sebastian Buchegger

using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace BankMessage
{
    using System;
    using System.Collections.Generic;

    public class Message
    {
        public int Version { get; set; }

        public double Betrag { get; set; }

        [XmlIgnore]
        public DateTimeOffset Ablaufdatum { get; set; }

        [XmlElement("Ablaufdatum")]
        public string AblaufdatumStr {
            get { return Ablaufdatum.ToString("O"); }
            set {Ablaufdatum = DateTimeOffset.Parse(value); }
        }

        public string AbsenderBankId { get; set; }

        public string EmpfaengerBankId { get; set; }

        public string Waehrung { get; set; }

        public string AbsenderKonto { get; set; }

        public string EmpfaengerKonto { get; set; }

        public TransactionType TransaktionsTyp { get; set; }

        public string Verwendungszweck { get; set; }

        public long MessageID { get; set; }



        public Message()
        {
            //var time = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public Message(int version, double betrag, DateTimeOffset ablaufdatum, string absenderBankId, string empfaengerBankId, string waehrung, string absenderKonto, string empfaengerKonto, TransactionType transaktionsTyp, string verwendungszweck, long messageId)
        {
            this.Version = version;
            this.Betrag = betrag;
            this.Ablaufdatum = ablaufdatum;
            this.AbsenderBankId = absenderBankId;
            this.EmpfaengerBankId = empfaengerBankId;
            this.Waehrung = waehrung;
            this.AbsenderKonto = absenderKonto;
            this.EmpfaengerKonto = empfaengerKonto;
            this.TransaktionsTyp = transaktionsTyp;
            this.Verwendungszweck = verwendungszweck;
            this.MessageID = messageId;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                $"Message: Id:'{MessageID}' Betrag: '{Betrag}' Währung: '{Waehrung}' Ablaufdatum: '{Ablaufdatum.ToString("R", new CultureInfo("de-DE"))}'");
            sb.AppendLine(
                $"AbsenderBank: '{AbsenderBankId}' AbsenderKonto: '{AbsenderKonto}' EmpfängerBank: '{EmpfaengerBankId}' EmpfängerKonto: '{EmpfaengerKonto}'");
            sb.AppendLine($"TransactionType: '{TransaktionsTyp}' Verwendungszweck: '{Verwendungszweck}'");
            return sb.ToString();
        }

        private sealed class BankMessageEqualityComparer : IEqualityComparer<Message>
        {
            public bool Equals(Message x, Message y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false; //&& x.Ablaufdatum.Equals(y.Ablaufdatum)
                return x.Version == y.Version && x.Betrag.Equals(y.Betrag) && string.Equals(x.AbsenderBankId, y.AbsenderBankId) && string.Equals(x.EmpfaengerBankId, y.EmpfaengerBankId) && string.Equals(x.Waehrung, y.Waehrung) && string.Equals(x.AbsenderKonto, y.AbsenderKonto) && string.Equals(x.EmpfaengerKonto, y.EmpfaengerKonto) && x.TransaktionsTyp == y.TransaktionsTyp && string.Equals(x.Verwendungszweck, y.Verwendungszweck) && x.MessageID == y.MessageID;
            }

            public int GetHashCode(Message obj)
            {
                unchecked
                {
                    var hashCode = obj.Version;
                    hashCode = (hashCode * 397) ^ obj.Betrag.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Ablaufdatum.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.AbsenderBankId != null ? obj.AbsenderBankId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.EmpfaengerBankId != null ? obj.EmpfaengerBankId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Waehrung != null ? obj.Waehrung.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.AbsenderKonto != null ? obj.AbsenderKonto.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.EmpfaengerKonto != null ? obj.EmpfaengerKonto.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int)obj.TransaktionsTyp;
                    hashCode = (hashCode * 397) ^ (obj.Verwendungszweck != null ? obj.Verwendungszweck.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.MessageID.GetHashCode();
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<Message> BankMessageComparerInstance = new BankMessageEqualityComparer();

        public static IEqualityComparer<Message> BankMessageComparer
        {
            get
            {
                return BankMessageComparerInstance;
            }
        }
    }
}
