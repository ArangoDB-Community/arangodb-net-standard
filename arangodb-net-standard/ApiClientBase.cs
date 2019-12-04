using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArangoDBNetStandard
{
    public abstract class ApiClientBase
    {
        private readonly IApiClientSerialization _serialization;

        /// <summary>
        /// Creates an instance of <see cref="ApiClientBase"/> using
        /// the provided serialization layer.
        /// </summary>
        /// <param name="serialization"></param>
        public ApiClientBase(IApiClientSerialization serialization)
        {
            _serialization = serialization;
        }

        protected async Task<ApiErrorException> GetApiErrorException(IApiClientResponse response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var error = _serialization.DeserializeFromStream<ApiErrorResponse>(stream);
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
            return _serialization.DeserializeFromStream<T>(stream);
        }

        protected byte[] GetContent<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues)
        {
            return _serialization.Serialize<T>(
                item,
                useCamelCasePropertyNames,
                ignoreNullValues);
        }
    }
}
