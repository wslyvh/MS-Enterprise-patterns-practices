
namespace wslyvh.Core.Interfaces.Serialization
{
    /// <summary>
    /// Abstraction of serialization logic.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the specified object to an XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <returns></returns>
        string Serialize<T>(T objectToSerialize) where T : class;

        /// <summary>
        /// Deserializes the specified XML to an object of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input XML.</param>
        /// <returns></returns>
        T Deserialize<T>(string input) where T : class;
    }
}
