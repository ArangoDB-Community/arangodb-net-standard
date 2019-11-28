using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArangoDBNetStandard
{
    public abstract class ApiClientBase
    {
        private readonly IContentSerialization _contentSerializer;

        public ApiClientBase(IContentSerialization contentSerializer)
        {
            _contentSerializer = contentSerializer;
        }

        protected async Task<ApiErrorException> GetApiErrorException(IApiClientResponse response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
            return new ApiErrorException(error);
        }

        protected void ValidateDocumentId(string documentId)
        {
            if (documentId.Split('/').Length != 2)
            {
                throw new ArgumentException("A valid document ID has two parts, split by '/'. + " +
                    "" + documentId + " is not a valid document ID. Maybe the document key was used by mistake?");
            }
        }

        protected T DeserializeJsonFromStream<T>(Stream stream)
        {
            return _contentSerializer.DeserializeJsonFromStream<T>(stream);
        }

        protected StringContent GetStringContent<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues)
        {
            string json = _contentSerializer.SerializeToJson<T>(
                item,
                useCamelCasePropertyNames,
                ignoreNullValues);
            return new StringContent(json);
        }
    }
}
