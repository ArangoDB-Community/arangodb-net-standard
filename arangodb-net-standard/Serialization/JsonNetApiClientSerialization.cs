using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

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
                var settings = new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                };
                var js = JsonSerializer.Create(settings);

                T result = js.Deserialize<T>(jtr);

                return result;
            }
        }

        /// <summary>
        /// Asynchronously deserializes the data structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public override async Task<T> DeserializeFromStreamAsync<T>(Stream stream)
        {
            T result = DeserializeFromStream<T>(stream);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>

        public override byte[] Serialize<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            string json = SerializeToString(item, serializationOptions);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Asynchronously serializes the specified object to a sequence of bytes,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public override async Task<byte[]> SerializeAsync<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            var result = Serialize<T>(item,serializationOptions);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Serializes an object to JSON string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>

        public override string SerializeToString<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            // When no options passed use the default.
            if (serializationOptions == null)
            {
                serializationOptions = DefaultOptions;
            }

            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = serializationOptions.IgnoreMissingMember ? MissingMemberHandling.Ignore : MissingMemberHandling.Error,
                NullValueHandling = serializationOptions.IgnoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            if (serializationOptions.UseStringEnumConversion)
            {
                var stringEnumConverter = new StringEnumConverter();
                jsonSettings.Converters.Add(stringEnumConverter);
            }

            if (serializationOptions.UseCamelCasePropertyNames)
            {
                jsonSettings.ContractResolver = new CamelCasePropertyNamesExceptDictionaryContractResolver(serializationOptions);
            }

            string json = JsonConvert.SerializeObject(item, jsonSettings);

            return json;
        }

        /// <summary>
        /// Asynchronously serializes the specified object to a JSON string,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public override async Task<string> SerializeToStringAsync<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            var result = SerializeToString(item, serializationOptions);
            return await Task.FromResult(result);
        }
    }
}
