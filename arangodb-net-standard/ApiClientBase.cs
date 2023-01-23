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
        protected async Task<ApiErrorException> GetApiErrorExceptionAsync(IApiClientResponse response)
        {
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            try
            {
                var error = await _serialization.DeserializeFromStreamAsync<ApiErrorResponse>(stream).ConfigureAwait(false);
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

        protected async Task<T> DeserializeJsonFromStreamAsync<T>(Stream stream)
        {
            try
            {
                return await  _serialization.DeserializeFromStreamAsync<T>(stream).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new SerializationException($"An error occured while Deserializing the data response from Arango. See InnerException for more details.", e);
            }
        }

        protected async Task<byte[]> GetContentAsync<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            try
            {
                return await _serialization.SerializeAsync(item, serializationOptions).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }

        protected async Task<string> GetContentStringAsync<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            try
            {
                return await _serialization.SerializeToStringAsync(item, serializationOptions).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }

    }
}
