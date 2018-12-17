using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace wslyvh.Core.Serialization
{
    public class DataSerializer : Serializer
    {
        public override string Serialize<T>(T objectToSerialize)
        {
            Guard.ArgumentIsNotNull(objectToSerialize, "objectToSerialize");

            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memoryStream, objectToSerialize);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public override T Deserialize<T>(string input)
        {
            Guard.ArgumentIsNotNull(input, "input");
            
            var serializer = new DataContractSerializer(typeof(T));
            return serializer.ReadObject(XElement.Parse(input).CreateReader()) as T;
        }
    }
}
