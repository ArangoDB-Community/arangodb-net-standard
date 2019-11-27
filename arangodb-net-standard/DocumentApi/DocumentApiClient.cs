using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using ArangoDBNetStandard.Transport;

namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Provides access to ArangoDB document API.
    /// </summary>
    public class DocumentApiClient : ApiClientBase
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
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PostDocumentResponse<T>> PostDocumentAsync<T>(string collectionName, T document, PostDocumentsQuery query = null)
        {
            string uriString = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            var content = GetStringContent(document, false, false);
            using (var response = await _client.PostAsync(uriString, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDocumentResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Post multiple documents in a single request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PostDocumentsResponse<T>> PostDocumentsAsync<T>(string collectionName, IList<T> documents, PostDocumentsQuery query = null)
        {
            string uriString = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            StringContent content = GetStringContent(documents, false, false);
            using (var response = await _client.PostAsync(uriString, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDocumentsResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Replace multiple documents.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PostDocumentsResponse<T>> PutDocumentsAsync<T>(string collectionName, IList<T> documents, PutDocumentsQuery query = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetStringContent(documents, false, false);
            using (var response = await _client.PutAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDocumentsResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
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
        public async Task<PostDocumentResponse<T>> PutDocumentAsync<T>(string documentId, T doc, PutDocumentsQuery opts = null)
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
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDocumentResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
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
            return await GetDocumentAsync<T>($"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}");
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
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var document = DeserializeJsonFromStream<T>(stream);
                return document;
            }
            throw await GetApiErrorException(response);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentResponse{object}"/>. Its value will be <see cref="null"/> when 
        /// <see cref="DeleteDocumentsQuery.ReturnOld"/> is either <see cref="false"/> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse<object>> DeleteDocumentAsync(string collectionName, string documentKey, DeleteDocumentsQuery query = null)
        {
            return await DeleteDocumentAsync<object>($"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}", query);
        }

        /// <summary>
        /// Delete a document based on its document ID.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentResponse{object}"/>. Its value will be <see cref="null"/> when 
        /// <see cref="DeleteDocumentsQuery.ReturnOld"/> is either <see cref="false"/> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="documentId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse<object>> DeleteDocumentAsync(string documentId, DeleteDocumentsQuery query = null)
        {
            return await DeleteDocumentAsync<object>(documentId, query);
        }

        /// <summary>
        /// Delete multiple documents based on the passed document selectors.
        /// A document selector is either the document ID or the document Key.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentsResponse{object}"/>. These will be <see cref="null"/> when 
        /// <see cref="DeleteDocumentsQuery.ReturnOld"/> is either <see cref="false"/> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="collectionName"></param>
        /// <param name="selectors"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentsResponse<object>> DeleteDocumentsAsync(string collectionName, IList<string> selectors, DeleteDocumentsQuery query = null)
        {
            return await DeleteDocumentsAsync<object>(collectionName, selectors, query);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse<T>> DeleteDocumentAsync<T>(string collectionName, string documentKey, DeleteDocumentsQuery query = null)
        {
            return await DeleteDocumentAsync<T>($"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}", query);
        }

        /// <summary>
        /// Delete a document based on its document ID.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentResponse<T>> DeleteDocumentAsync<T>(string documentId, DeleteDocumentsQuery query = null)
        {
            ValidateDocumentId(documentId);
            string uri = _docApiPath + "/" + documentId;
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _client.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var responseModel = DeserializeJsonFromStream<DeleteDocumentResponse<T>>(stream);
                    return responseModel;
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Delete multiple documents based on the passed document selectors.
        /// A document selector is either the document ID or the document Key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="selectors"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DeleteDocumentsResponse<T>> DeleteDocumentsAsync<T>(string collectionName, IList<string> selectors, DeleteDocumentsQuery query = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetStringContent(selectors, false, false);
            using (var response = await _client.DeleteAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var responseModel = DeserializeJsonFromStream<DeleteDocumentsResponse<T>>(stream);
                    return responseModel;
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Partially updates documents, the documents to update are specified
        /// by the _key attributes in the body objects.The body of the
        /// request must contain a JSON array of document updates with the
        /// attributes to patch(the patch documents). All attributes from the
        /// patch documents will be added to the existing documents if they do
        /// not yet exist, and overwritten in the existing documents if they do
        /// exist there.
        /// Setting an attribute value to null in the patch documents will cause a
        /// value of null to be saved for the attribute by default.
        /// If ignoreRevs is false and there is a _rev attribute in a
        /// document in the body and its value does not match the revision of
        /// the corresponding document in the database, the precondition is
        /// violated.
        /// PATCH/_api/document/{collection}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="patches"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<PatchDocumentsResponse<U>>> PatchDocumentsAsync<T, U>(string collectionName, IList<T> patches, PatchDocumentsQuery query = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetStringContent(selectors, false, false);
            using (var response = await _client.PatchAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<IList<PatchDocumentsResponse<U>>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

    }
}
