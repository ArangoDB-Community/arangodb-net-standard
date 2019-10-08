using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ArangoDBNetStandard
{
    public abstract class ApiClientBase
    {
        protected static T DeserializeJsonFromStream<T>(Stream stream, bool useCamelCasePropertyNames = false, bool ignoreNullValues = false)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
                };
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        protected StringContent GetStringContent<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues)
        {
            string json = JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            });
            return new StringContent(json);
        }

    }
}
