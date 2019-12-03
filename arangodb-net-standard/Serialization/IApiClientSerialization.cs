using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Defines a serialization layer used for the content in transport.
    /// </summary>
    public interface IApiClientSerialization
    {
        /// <summary>
        /// Deserializes the JSON structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        T DeserializeJsonFromStream<T>(Stream stream);

        /// <summary>
        /// Serializes the specified object to a JSON string encoded as UTF-8 bytes,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="useCamelCasePropertyNames">Whether property names should be camel cased.</param>
        /// <param name="ignoreNullValues">Whether null values should be ignored.</param>
        /// <returns></returns>
        byte[] SerializeToJson<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues);
    }
}
