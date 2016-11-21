using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BankMessage;

namespace ForeignBankComponent
{
    public class SentMessageSerializer
    {
        private const string fileName = "sent_messages.xml";


        public Dictionary<long, Message> Load()
        {
            var dict = new Dictionary<long, Message>();

            if (!File.Exists(fileName)) // File not yet written
            {
                return dict;
            }

            var serializer = new XmlSerializer(typeof(List<Message>));
            List<Message> messagesList;
            using (TextReader tw = new StreamReader(fileName))
            {
                messagesList = (List<Message>) serializer.Deserialize(tw);
            }
            messagesList.ForEach(x => dict.Add(x.MessageID, x));
            return dict;
        }

        public void Store(Dictionary<long, Message> messages)
        {
            var messagesList = messages.Values.ToList();

            var serializer = new XmlSerializer(typeof(List<Message>));

            using (TextWriter tw = new StreamWriter(fileName, false))
            {
                serializer.Serialize(tw, messagesList);
            }
        }
    }
}