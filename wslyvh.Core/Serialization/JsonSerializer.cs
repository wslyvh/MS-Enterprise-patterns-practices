using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace wslyvh.Core.Serialization
{
    public class JsonSerializer : Serializer
    {
        public override string Serialize<T>(T objectToSerialize)
        {
            Guard.ArgumentIsNotNull(objectToSerialize, "objectToSerialize");

            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(memoryStream, objectToSerialize);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public override T Deserialize<T>(string input)
        {
            Guard.ArgumentIsNotNull(input, "input");
            
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(input)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}
