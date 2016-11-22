using System.IO;
using System.Xml.Serialization;

namespace Components.Common
{
    public class XmlSerializer<T>
    {
        private readonly string _filename;
        
        public XmlSerializer(string fileName)
        {
            _filename = fileName;
        }

        public T Load()
        {
            var obj = default(T);

            if (!File.Exists(_filename)) // File not yet written
            {
                return obj;
            }

            var serializer = new XmlSerializer(typeof(T));
            
            using (TextReader tw = new StreamReader(_filename))
            {
                obj = (T) serializer.Deserialize(tw);
            }
            return obj;
        }

        public void Store(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (TextWriter tw = new StreamWriter(_filename, false))
            {
                serializer.Serialize(tw, obj);
            }
        }
    }
}