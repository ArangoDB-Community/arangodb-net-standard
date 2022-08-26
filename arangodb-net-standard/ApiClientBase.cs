﻿using ArangoDBNetStandard.Serialization;
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
            try
            {
                if (response.Content.Headers.ContentType?.MediaType?.Contains("json",
                        StringComparison.InvariantCultureIgnoreCase) ?? false)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var error = _serialization.DeserializeFromStream<ApiErrorResponse>(stream);
                    return new ApiErrorException(error);
                }
                return new ApiErrorException($"HTTP Status Code: {response.StatusCode}");
            }
            catch (Exception e)
            {
                return new ApiErrorException($"HTTP Status Code: {response.StatusCode}", e);
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

        protected byte[] GetContent<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            try
            {
                return _serialization.Serialize(item, serializationOptions);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }

        protected string GetContentString<T>(T item, ApiClientSerializationOptions serializationOptions)
        {
            try
            {
                return _serialization.SerializeToString(item, serializationOptions);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }

    }
}
