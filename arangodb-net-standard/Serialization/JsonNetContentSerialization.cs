using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    public class JsonNetContentSerialization : IContentSerialization
    {
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
