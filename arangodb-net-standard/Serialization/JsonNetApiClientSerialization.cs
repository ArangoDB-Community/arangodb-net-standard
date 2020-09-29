using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Implements a <see cref="IApiClientSerialization"/> that uses Json.NET.
    /// </summary>
    public class JsonNetApiClientSerialization : ApiClientSerialization
    {
        /// <summary>
        /// Deserializes the JSON structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public override T DeserializeFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                return default(T);
            }

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();

                T result = js.Deserialize<T>(jtr);

                return result;
            }
        }

        /// <summary>
        /// Serializes the specified object to a JSON string encoded as UTF-8 bytes,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="serializationOptions"></param>
        /// <returns></returns>
        protected override byte[] SerializeItem<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = serializationOptions.IgnoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            if (serializationOptions.UseCamelCasePropertyNames)
            {
                jsonSettings.ContractResolver = new CamelCasePropertyNamesExceptDictionaryContractResolver();
            }

            string json = JsonConvert.SerializeObject(item, jsonSettings);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}
