using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// The Api client serialization abastract class.
    /// Used as a base to implement custom serialization.
    /// </summary>
    public abstract class ApiClientSerialization : IApiClientSerialization
    {
        /// <summary>
        /// The default serialization options.
        /// </summary>
        public virtual ApiClientSerializationOptions DefaultOptions => new ApiClientSerializationOptions(false, true);

        /// <summary>
        /// Deserializes the data structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public abstract T DeserializeFromStream<T>(Stream stream);

        /// <summary>
        /// Serializes the specified object to a sequence of bytes,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public abstract byte[] Serialize<T>(T item, ApiClientSerializationOptions serializationOptions);
    }
}
