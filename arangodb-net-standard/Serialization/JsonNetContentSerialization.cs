using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Implements a <see cref="IContentSerialization"/> that uses Json.NET.
    /// </summary>
    public class JsonNetContentSerialization : IContentSerialization
    {
        /// <summary>
        /// Deserializes the JSON structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public virtual T DeserializeJsonFromStream<T>(Stream stream)
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
        /// Serializes the specified object to a JSON string,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="useCamelCasePropertyNames">Whether property names should be camel cased.</param>
        /// <param name="ignoreNullValues">Whether null values should be ignored.</param>
        /// <returns></returns>
        public virtual string SerializeToJson<T>(
            T item,
            bool useCamelCasePropertyNames,
            bool ignoreNullValues)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            if (useCamelCasePropertyNames)
            {
                jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            string json = JsonConvert.SerializeObject(item, jsonSettings);

            return json;
        }
    }
}
