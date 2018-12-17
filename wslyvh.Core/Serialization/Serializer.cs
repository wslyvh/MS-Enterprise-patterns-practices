using System;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Interfaces.Serialization;

namespace wslyvh.Core.Serialization
{
    public abstract class Serializer : ISerializer
    {
        public abstract string Serialize<T>(T objectToSerialize) where T : class;

        public abstract T Deserialize<T>(string input) where T : class;
    }
}
