using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BankMessage;

namespace ForeignBankComponent
{
    public class SentMessageSerializer
    {
        private readonly string _fileName;

        public SentMessageSerializer(string fileName = "sent_message.xml")
        {
            _fileName = fileName;
        }


        public Dictionary<long, Message> Load()
        {
            var dict = new Dictionary<long, Message>();

            if (!File.Exists(_fileName)) // File not yet written
            {
                return dict;
            }

            var serializer = new XmlSerializer(typeof(List<Message>));
            List<Message> messagesList;
            using (TextReader tw = new StreamReader(_fileName))
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

            using (TextWriter tw = new StreamWriter(_fileName, false))
            {
                serializer.Serialize(tw, messagesList);
            }
        }
    }
}