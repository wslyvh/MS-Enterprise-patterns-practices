using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace wslyvh.Core.Serialization
{
    public class XmlDataSerializer : Serializer
    {
        public override string Serialize<T>(T objectToSerialize)
        {
            Guard.ArgumentIsNotNull(objectToSerialize, "objectToSerialize");

            var serializer = new XmlSerializer(objectToSerialize.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var stream = new MemoryStream())
            {
                var writer = XmlWriter.Create(stream);
                serializer.Serialize(writer, objectToSerialize, ns);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public override T Deserialize<T>(string input)
        {
            Guard.ArgumentIsNotNull(input, "input");

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(input))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
