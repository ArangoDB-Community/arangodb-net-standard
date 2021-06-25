using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Provides access to ArangoDB document API.
    /// </summary>
    public class DocumentApiClient : ApiClientBase, IDocumentApiClient
    {
        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _docApiPath = "_api/document";

        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// Creates an instance of <see cref="DocumentApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public DocumentApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="DocumentApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public DocumentApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
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
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual Task<PostDocumentResponse<T>> PostDocumentAsync<T>(
            string collectionName,
            T document,
            PostDocumentsQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            return PostDocumentAsync<T, T>(
                collectionName,
                document,
                query,
                serializationOptions);
        }

        /// <summary>
        /// Post a single document with the possibility to specify a different type
        /// for the new document object returned in the response.
        /// </summary>
        /// <typeparam name="T">The type of the post object used to record a new document.</typeparam>
        /// <typeparam name="U">Type of the returned document, only applies when
        /// <see cref="PostDocumentsQuery.ReturnNew"/> or <see cref="PostDocumentsQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="document"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PostDocumentResponse<U>> PostDocumentAsync<T, U>(
            string collectionName,
            T document,
            PostDocumentsQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            string uriString = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            var content = GetContent(document, serializationOptions);
            using (var response = await _client.PostAsync(uriString, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PostDocumentResponse<U>>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Post multiple documents in a single request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PostDocumentsResponse<T>> PostDocumentsAsync<T>(
            string collectionName,
            IList<T> documents,
            PostDocumentsQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            string uriString = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            var content = GetContent(documents, serializationOptions);
            using (var response = await _client.PostAsync(uriString, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (query != null && query.Silent.HasValue && query.Silent.Value)
                    {
                        return PostDocumentsResponse<T>.Empty();
                    }
                    else
                    {
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        return DeserializeJsonFromStream<PostDocumentsResponse<T>>(stream);
                    }
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Replace multiple documents.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PutDocumentsResponse<T>> PutDocumentsAsync<T>(
            string collectionName,
            IList<T> documents,
            PutDocumentsQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetContent(documents, serializationOptions);
            using (var response = await _client.PutAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (query != null && query.Silent.HasValue && query.Silent.Value)
                    {
                        return PutDocumentsResponse<T>.Empty();
                    }
                    else
                    {
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        return DeserializeJsonFromStream<PutDocumentsResponse<T>>(stream);
                    }
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Replaces the document with the provided document ID with the one in
        /// the body, provided there is such a document and no precondition is
        /// violated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <param name="doc"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PutDocumentResponse<T>> PutDocumentAsync<T>(
            string documentId,
            T doc,
            PutDocumentQuery opts = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            ValidateDocumentId(documentId);
            string uri = _docApiPath + "/" + documentId;
            if (opts != null)
            {
                uri += "?" + opts.ToQueryString();
            }
            var content = GetContent(doc, serializationOptions);
            using (var response = await _client.PutAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PutDocumentResponse<T>>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Replaces the document based on its Document ID with the one in
        /// the body, provided there is such a document and no precondition is
        /// violated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="doc"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public virtual Task<PutDocumentResponse<T>> PutDocumentAsync<T>(
            string collectionName,
            string documentKey,
            T doc,
            PutDocumentQuery opts = null)
        {
            return PutDocumentAsync<T>(
                $"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}",
                doc,
                opts);
        }

        /// <summary>
        /// Get an existing document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <returns></returns>
        public virtual async Task<T> GetDocumentAsync<T>(string collectionName, string documentKey)
        {
            return await GetDocumentAsync<T>(
                $"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}").ConfigureAwait(false);
        }

        /// <summary>
        /// Get an existing document based on its Document ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public virtual async Task<T> GetDocumentAsync<T>(string documentId)
        {
            ValidateDocumentId(documentId);
            var response = await _client.GetAsync(_docApiPath + "/" + documentId).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var document = DeserializeJsonFromStream<T>(stream);
                return document;
            }
            throw await GetApiErrorException(response).ConfigureAwait(false);
        }

        /// <summary>
        /// Get multiple documents.
        /// </summary>
        /// <typeparam name="T">The type of the documents
        /// deserialized from the response.</typeparam>
        /// <param name="collectionName">Collection name</param>
        /// <param name="selectors">Document keys to fetch documents for</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetDocumentsAsync<T>(
            string collectionName,
            IList<string> selectors)
        {
            string uri = $"{_docApiPath}/{WebUtility.UrlEncode(collectionName)}?onlyget=true";

            var content = GetContent(selectors, new ApiClientSerializationOptions(false, true));

            using (var response = await _client.PutAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var documents = DeserializeJsonFromStream<List<T>>(stream);
                    return documents;
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentResponse{T}"/>. Its value will be <c>null</c> when 
        /// <see cref="DeleteDocumentQuery.ReturnOld"/> is either <c>false</c> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDocumentResponse<object>> DeleteDocumentAsync(
            string collectionName,
            string documentKey,
            DeleteDocumentQuery query = null)
        {
            return await DeleteDocumentAsync<object>(
                $"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}",
                query).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a document based on its document ID.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentResponse{T}"/>. Its value will be <c>null</c> when 
        /// <see cref="DeleteDocumentQuery.ReturnOld"/> is either <c>false</c> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="documentId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDocumentResponse<object>> DeleteDocumentAsync(
            string documentId,
            DeleteDocumentQuery query = null)
        {
            return await DeleteDocumentAsync<object>(documentId, query).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDocumentResponse<T>> DeleteDocumentAsync<T>(
            string collectionName,
            string documentKey,
            DeleteDocumentQuery query = null)
        {
            return await DeleteDocumentAsync<T>(
                $"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}",
                query).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a document based on its document ID.
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDocumentResponse<T>> DeleteDocumentAsync<T>(
            string documentId,
            DeleteDocumentQuery query = null)
        {
            ValidateDocumentId(documentId);
            string uri = _docApiPath + "/" + documentId;
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _client.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var responseModel = DeserializeJsonFromStream<DeleteDocumentResponse<T>>(stream);
                    return responseModel;
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete multiple documents based on the passed document selectors.
        /// A document selector is either the document ID or the document Key.
        /// </summary>
        /// <remarks>
        /// This method overload is provided as a convenience when the client does not care about the type of <see cref="DeleteDocumentResponse{T}.Old"/>
        /// in the returned <see cref="DeleteDocumentsResponse{T}"/>. These will be <c>null</c> when 
        /// <see cref="DeleteDocumentsQuery.ReturnOld"/> is either <c>false</c> or not set, so this overload is useful in the default case 
        /// when deleting documents.
        /// </remarks>
        /// <param name="collectionName"></param>
        /// <param name="selectors"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDocumentsResponse<object>> DeleteDocumentsAsync(
            string collectionName,
            IList<string> selectors,
            DeleteDocumentsQuery query = null)
        {
            return await DeleteDocumentsAsync<object>(collectionName, selectors, query).ConfigureAwait(false);
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
        public virtual async Task<DeleteDocumentsResponse<T>> DeleteDocumentsAsync<T>(
            string collectionName,
            IList<string> selectors,
            DeleteDocumentsQuery query = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetContent(selectors, new ApiClientSerializationOptions(false, false));
            using (var response = await _client.DeleteAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (query != null && query.Silent.HasValue && query.Silent.Value)
                    {
                        return DeleteDocumentsResponse<T>.Empty();
                    }
                    else
                    {
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        return DeserializeJsonFromStream<DeleteDocumentsResponse<T>>(stream);
                    }
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Partially updates documents, the documents to update are specified
        /// by the _key attributes in the body objects. All attributes from the
        /// patch documents will be added to the existing documents if they do
        /// not yet exist, and overwritten in the existing documents if they do
        /// exist there.
        /// Setting an attribute value to null in the patch documents will cause a
        /// value of null to be saved for the attribute by default.
        /// If ignoreRevs is false and there is a _rev attribute in a
        /// document in the body and its value does not match the revision of
        /// the corresponding document in the database, the precondition is
        /// violated.
        /// PATCH /_api/document/{collection}
        /// </summary>
        /// <typeparam name="T">Type of the patch object used to partially update documents.</typeparam>
        /// <typeparam name="U">Type of the returned documents, only applies when
        /// <see cref="PatchDocumentsQuery.ReturnNew"/> or <see cref="PatchDocumentsQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="patches"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PatchDocumentsResponse<U>> PatchDocumentsAsync<T, U>(
            string collectionName,
            IList<T> patches,
            PatchDocumentsQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            string uri = _docApiPath + "/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetContent(patches, serializationOptions);
            using (var response = await _client.PatchAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (query != null && query.Silent.HasValue && query.Silent.Value)
                    {
                        return PatchDocumentsResponse<U>.Empty();
                    }
                    else
                    {
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        return DeserializeJsonFromStream<PatchDocumentsResponse<U>>(stream);
                    }
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Partially updates the document identified by document-handle.
        /// The body of the request must contain a JSON document with the
        /// attributes to patch(the patch document). All attributes from the
        /// patch document will be added to the existing document if they do not
        /// yet exist, and overwritten in the existing document if they do exist
        /// there.
        /// PATCH/_api/document/{document-handle}
        /// </summary>
        /// <typeparam name="T">Type of the patch object used to partially update a document.</typeparam>
        /// <typeparam name="U">Type of the returned document, only applies when
        /// <see cref="PatchDocumentQuery.ReturnNew"/> or <see cref="PatchDocumentQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PatchDocumentResponse<U>> PatchDocumentAsync<T, U>(
            string collectionName,
            string documentKey,
            T body,
            PatchDocumentQuery query = null)
        {
            string documentHandle = WebUtility.UrlEncode(collectionName) +
                "/" + WebUtility.UrlEncode(documentKey);

            return await PatchDocumentAsync<T, U>(documentHandle, body, query).ConfigureAwait(false);
        }

        /// <summary>
        /// Partially updates the document identified by document-handle.
        /// The body of the request must contain a JSON document with the
        /// attributes to patch(the patch document). All attributes from the
        /// patch document will be added to the existing document if they do not
        /// yet exist, and overwritten in the existing document if they do exist
        /// there.
        /// PATCH/_api/document/{document-handle}
        /// </summary>
        /// <typeparam name="T">Type of the patch object used to partially update a document.</typeparam>
        /// <typeparam name="U">Type of the returned document, only applies when
        /// <see cref="PatchDocumentQuery.ReturnNew"/> or <see cref="PatchDocumentQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="documentId"></param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PatchDocumentResponse<U>> PatchDocumentAsync<T, U>(
            string documentId,
            T body,
            PatchDocumentQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            ValidateDocumentId(documentId);
            string uriString = _docApiPath + "/" + documentId;
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            var content = GetContent(body, serializationOptions);
            using (var response = await _client.PatchAsync(uriString, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PatchDocumentResponse<U>>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Like GET, but only returns the header fields and not the body. You
        /// can use this call to get the current revision of a document or check if
        /// the document was deleted.
        /// HEAD /_api/document/{document-handle}
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="documentKey"></param>
        /// <param name="headers"></param>
        /// <remarks>
        /// 200: is returned if the document was found. 
        /// 304: is returned if the “If-None-Match” header is given and the document has the same version. 
        /// 404: is returned if the document or collection was not found. 
        /// 412: is returned if an “If-Match” header is given and the found document has a different version. The response will also contain the found document’s current revision in the Etag header.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<HeadDocumentResponse> HeadDocumentAsync(
            string collectionName,
            string documentKey,
            HeadDocumentHeader headers = null)
        {
            return await HeadDocumentAsync(
                $"{WebUtility.UrlEncode(collectionName)}/{WebUtility.UrlEncode(documentKey)}",
                headers).ConfigureAwait(false);
        }

        /// <summary>
        /// Like GET, but only returns the header fields and not the body. You
        /// can use this call to get the current revision of a document or check if
        /// the document was deleted.
        /// HEAD/_api/document/{document-handle}
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="headers">Object containing a dictionary of Header keys and values</param>
        /// <exception cref="System.ArgumentException">Document ID is invalid.</exception>
        /// <remarks>
        /// 200: is returned if the document was found. 
        /// 304: is returned if the “If-None-Match” header is given and the document has the same version. 
        /// 404: is returned if the document or collection was not found. 
        /// 412: is returned if an “If-Match” header is given and the found document has a different version. The response will also contain the found document’s current revision in the Etag header.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<HeadDocumentResponse> HeadDocumentAsync(
            string documentId,
            HeadDocumentHeader headers = null)
        {
            ValidateDocumentId(documentId);
            string uri = _docApiPath + "/" + documentId;
            WebHeaderCollection headerCollection;
            if (headers == null)
            {
                headerCollection = new WebHeaderCollection();
            }
            else
            {
                headerCollection = headers.ToWebHeaderCollection();
            }
            using (var response = await _client.HeadAsync(uri, headerCollection).ConfigureAwait(false))
            {
                return new HeadDocumentResponse
                {
                    Code = (HttpStatusCode)response.StatusCode,
                    Etag = response.Headers.ETag
                };
            }
        }
    }
}
