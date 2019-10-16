using ArangoDBNetStandard.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard
{
    public abstract class ApiClientBase
    {
        protected async Task HandleApiError(IApiClientResponse response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
            throw new ApiErrorException(error);
        }

        protected void ValidateDocumentId(string documentId)
        {
            if (documentId.Split('/').Length != 2)
            {
                throw new ArgumentException("A valid document ID has two parts, split by '/'. + " +
                    "" + documentId + " is not a valid document ID. Maybe the document key was used by mistake?");
            }
        }

        protected T DeserializeJsonFromStream<T>(Stream stream, bool useCamelCasePropertyNames = false, bool ignoreNullValues = false)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer
                {
                    NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
                };
                if (useCamelCasePropertyNames)
                {
                    js.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        protected StringContent GetStringContent<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues)
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
            return new StringContent(json);
        }

    }
}
