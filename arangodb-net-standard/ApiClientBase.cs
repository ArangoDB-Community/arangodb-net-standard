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

        /// <summary>
        /// Gets an <see cref="ApiErrorException"/> from the provided error response.
        /// </summary>
        /// <param name="response">The error response from ArangoDB.</param>
        /// <returns></returns>
        protected async Task<ApiErrorException> GetApiErrorException(IApiClientResponse response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            try
            {
                var error = _serialization.DeserializeFromStream<ApiErrorResponse>(stream);
                return new ApiErrorException(error);
            }
            catch (Exception e)
            {
                throw new SerializationException($"An error occured while Deserializing an error response from Arango. See InnerException for more details.", e);
            }
        }

        /// <summary>
        /// Checks whether the provided document ID is in the correct form
        /// of "{collection}/{key}".
        /// </summary>
        /// <exception cref="ArgumentException">The document ID is invalid</exception>
        /// <param name="documentId">The document ID to validate.</param>
        protected void ValidateDocumentId(string documentId)
        {
            if (documentId.Split('/').Length != 2)
            {
                throw new ArgumentException("A valid document ID has two parts, split by '/'. \"" +
                    documentId + "\" is not a valid document ID. Maybe the document key was used by mistake?");
            }
        }

        protected T DeserializeJsonFromStream<T>(Stream stream)
        {
            try
            {
                return _serialization.DeserializeFromStream<T>(stream);
            }
            catch (Exception e)
            {
                throw new SerializationException($"An error occured while Deserializing the data response from Arango. See InnerException for more details.", e);
            }
        }

        protected byte[] GetContent<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues)
        {
            try
            {
                return _serialization.Serialize<T>(
                    item,
                    useCamelCasePropertyNames,
                    ignoreNullValues);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }
    }
}
