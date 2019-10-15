using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using ArangoDBNetStandard.Transport;

namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Provides access to ArangoDB document API.
    /// </summary>
    public class DocumentApiClient: ApiClientBase
    {
        private readonly string _docApiPath = "_api/document";
        private IApiClientTransport _client;

        /// <summary>
        /// Create an instance of <see cref="DocumentApiClient"/>.
        /// </summary>
        /// <param name="client"></param>
        public DocumentApiClient(IApiClientTransport client)
        {
            _client = client;
        }

        /// <summary>
        /// Post a single document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="document"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<PostDocumentResponse<T>> PostDocumentAsync<T>(string collectionName, T document, PostDocumentsOptions options = null)
        {
            string uriString = _docApiPath + "/" + collectionName;
            if (options != null)
            {
                uriString += "?" + options.ToQueryString();
            }
            var content = GetStringContent(document, false, false);
            using (var response = await _client.PostAsync(uriString, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostDocumentResponse<T>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Post multiple documents in a single request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<PostDocumentsResponse<T>> PostDocumentsAsync<T>(string collectionName, IList<T> documents, PostDocumentsOptions options = null)
        {
            string uriString = _docApiPath + "/" + collectionName;
            if (options != null)
            {
                uriString += "?" + options.ToQueryString();
            }
            StringContent content = GetStringContent(documents, false, false);
            using (var response = await _client.PostAsync(uriString, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostDocumentsResponse<T>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Replace multiple documents.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<PostDocumentsResponse<T>> PutDocumentsAsync<T>(string collectionName, IList<T> documents, PutDocumentsOptions options = null)
        {
            string uri = _docApiPath + "/" + collectionName;
            if (options != null)
            {
                uri += "?" + options.ToQueryString();
            }
            var content = GetStringContent(documents, false, false);
            using (var response = await _client.PutAsync(uri, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostDocumentsResponse<T>>(stream);
                }
                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Replace a document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <param name="doc"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public async Task<PostDocumentResponse<T>> PutDocumentAsync<T>(string documentId, T doc, PutDocumentsOptions opts = null)
        {
            ValidateDocumentId(documentId);
            string uri = _docApiPath + "/" + documentId;
            if (opts != null)
            {
                uri += "?" + opts.ToQueryString();
            }
            var content = GetStringContent(doc, false, false);
            using (var response = await _client.PutAsync(uri, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostDocumentResponse<T>>(stream);
                }
                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }
      
        /// <summary>
        /// Get an existing document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <returns></returns>
        public async Task<T> GetDocumentAsync<T>(string collectionName, string documentKey)
        {
            return await GetDocumentAsync<T>($"{collectionName}/{documentKey}");
        }

        /// <summary>
        /// Get an existing document based on its Document ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<T> GetDocumentAsync<T>(string documentId)
        {
            ValidateDocumentId(documentId);
            var response = await _client.GetAsync(_docApiPath + "/" + documentId);
            var stream = await response.Content.ReadAsStreamAsync();
            if (response.IsSuccessStatusCode)
            {
                var document = DeserializeJsonFromStream<T>(stream);
                return document;
            }
            var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
            throw new ApiErrorException(error);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse> DeleteDocumentAsync(string collectionName, string documentKey)
        {
            return await DeleteDocumentAsync($"{collectionName}/{documentKey}");
        }

        /// <summary>
        /// Delete a document based on its document ID.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse> DeleteDocumentAsync(string documentId)
        {
            ValidateDocumentId(documentId);
            using (var response = await _client.DeleteAsync(_docApiPath + "/" + documentId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new DeleteDocumentResponse((int)response.StatusCode);
                }
                var stream = await response.Content.ReadAsStreamAsync();
                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }
    }
}
