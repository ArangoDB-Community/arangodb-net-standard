using ArangoDBNetStandard.GraphApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Graphs endpoints,
    /// implementing <see cref="IGraphApiClient"/>.
    /// </summary>
    public class GraphApiClient : ApiClientBase, IGraphApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _graphApiPath = "_api/gharial";

        /// <summary>
        /// Create an instance of <see cref="GraphApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public GraphApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Create an instance of <see cref="GraphApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public GraphApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        /// <summary>
        /// Creates a new graph in the graph module.
        /// POST /_api/gharial
        /// </summary>
        /// <param name="postGraphBody">The information of the graph to create.</param>
        /// <param name="query">Optional query parameters of the request.</param>
        /// <returns></returns>
        public virtual async Task<PostGraphResponse> PostGraphAsync(
            PostGraphBody postGraphBody,
            PostGraphQuery query = null)
        {
            string uri = _graphApiPath;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            var content = GetContent(postGraphBody, new ApiClientSerializationOptions(true, true));

            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostGraphResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Lists all graphs stored in this database.
        /// GET /_api/gharial
        /// </summary>
        /// <remarks>
        /// Note: The <see cref="GraphResult.Name"/> property is null for <see cref="GraphApiClient.GetGraphsAsync"/>
        /// in ArangoDB 4.5.2 and below, in which case you can use <see cref="GraphResult._key"/> instead.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<GetGraphsResponse> GetGraphsAsync()
        {
            using (var response = await _transport.GetAsync(_graphApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphsResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Deletes an existing graph object by name.
        /// Optionally all collections not used by other
        /// graphs can be deleted as well, using <see cref="DeleteGraphQuery"/>.
        /// DELETE /_api/gharial/{graph-name}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteGraphResponse> DeleteGraphAsync(
            string graphName,
            DeleteGraphQuery query = null)
        {
            string uriString = _graphApiPath + "/" + WebUtility.UrlEncode(graphName);
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            using (var response = await _transport.DeleteAsync(uriString))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteGraphResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Selects information for a given graph.
        /// Will return the edge definitions as well as the orphan collections.
        /// GET /_api/gharial/{graph}
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public virtual async Task<GetGraphResponse> GetGraphAsync(string graphName)
        {
            using (var response = await _transport.GetAsync(_graphApiPath + "/" + WebUtility.UrlEncode(graphName)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Lists all vertex collections within the given graph.
        /// GET /_api/gharial/{graph}/vertex
        /// </summary>
        /// <param name="graphName">The name of the graph.</param>
        /// <returns></returns>
        public virtual async Task<GetVertexCollectionsResponse> GetVertexCollectionsAsync(string graphName)
        {
            using (var response = await _transport.GetAsync(_graphApiPath + '/' + WebUtility.UrlEncode(graphName) + "/vertex"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetVertexCollectionsResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Lists all edge collections within this graph.
        /// GET /_api/gharial/{graph}/edge
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public virtual async Task<GetEdgeCollectionsResponse> GetEdgeCollectionsAsync(string graphName)
        {
            using (var response = await _transport.GetAsync(_graphApiPath + "/" + WebUtility.UrlEncode(graphName) + "/edge"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetEdgeCollectionsResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Adds an additional edge definition to the graph.
        /// This edge definition has to contain a collection and an array of
        /// each from and to vertex collections. An edge definition can only
        /// be added if this definition is either not used in any other graph, or
        /// it is used with exactly the same definition. It is not possible to
        /// store a definition “e” from “v1” to “v2” in the one graph, and “e”
        /// from “v2” to “v1” in the other graph.
        /// POST /_api/gharial/{graph}/edge
        /// </summary>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="body">The information of the edge definition.</param>
        /// <returns></returns>
        public virtual async Task<PostEdgeDefinitionResponse> PostEdgeDefinitionAsync(
            string graphName,
            PostEdgeDefinitionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) + "/edge";

            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostEdgeDefinitionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Adds a vertex collection to the set of orphan collections of the graph.
        /// If the collection does not exist, it will be created.
        /// POST /_api/gharial/{graph}/vertex
        /// </summary>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="body">The information of the vertex collection.</param>
        /// <returns></returns>
        public virtual async Task<PostVertexCollectionResponse> PostVertexCollectionAsync(
            string graphName,
            PostVertexCollectionBody body)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) + "/vertex";

            var content = GetContent(body, new ApiClientSerializationOptions(true, true));

            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostVertexCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Adds a vertex to the given collection.
        /// POST/_api/gharial/{graph}/vertex/{collection}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertex"></param>
        /// <param name="query"></param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PostVertexResponse<T>> PostVertexAsync<T>(
            string graphName,
            string collectionName,
            T vertex,
            PostVertexQuery query = null,
            ApiClientSerializationOptions serializationOptions = null)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            var content = GetContent(vertex, serializationOptions);
            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostVertexResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Remove one edge definition from the graph. This will only remove the
        /// edge collection, the vertex collections remain untouched and can still
        /// be used in your queries.
        /// DELETE/_api/gharial/{graph}/edge/{definition}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteEdgeDefinitionResponse> DeleteEdgeDefinitionAsync(
            string graphName,
            string collectionName,
            DeleteEdgeDefinitionQuery query = null)
        {
            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _transport.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteEdgeDefinitionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Removes a vertex collection from the graph and optionally deletes the collection,
        /// if it is not used in any other graph.
        /// It can only remove vertex collections that are no longer part of edge definitions,
        /// if they are used in edge definitions you are required to modify those first.
        /// DELETE/_api/gharial/{graph}/vertex/{collection}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteVertexCollectionResponse> DeleteVertexCollectionAsync(
            string graphName,
            string collectionName,
            DeleteVertexCollectionQuery query = null)
        {
            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _transport.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteVertexCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Creates an edge in an existing graph.
        /// The edge must contain a _from and _to value
        /// referencing valid vertices in the graph.
        /// The edge has to conform to the definition of the edge collection it is added to.
        /// POST /_api/gharial/{graph}/edge/{collection}
        /// </summary>
        /// <typeparam name="T">The type of the edge to create.
        /// Must contain valid _from and _to properties once serialized.
        /// <c>null</c> properties are preserved during serialization.</typeparam>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="collectionName">The name of the edge collection the edge belongs to.</param>
        /// <param name="edge">The edge to create.</param>
        /// <param name="query">Optional query parameters of the request.</param>
        /// <param name="serializationOptions">The serialization options. When the value is null the
        /// the serialization options should be provided by the serializer, otherwise the given options should be used.</param>
        /// <returns></returns>
        public virtual async Task<PostEdgeResponse<T>> PostEdgeAsync<T>(
            string graphName,
            string collectionName,
            T edge,
            PostEdgeQuery query = null,
            ApiClientSerializationOptions serializationOptions  = null)
        {
            var content = GetContent(edge, serializationOptions);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + WebUtility.UrlEncode(collectionName);

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostEdgeResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Gets an edge from the given graph using the edge collection and _key attribute.
        /// </summary>
        /// <typeparam name="T">The type of the edge document to deserialize to.</typeparam>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="collectionName">The name of the edge collection the edge belongs to.</param>
        /// <param name="edgeKey">The _key attribute of the edge.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<GetEdgeResponse<T>> GetEdgeAsync<T>(
            string graphName,
            string collectionName,
            string edgeKey,
            GetEdgeQuery query = null)
        {
            return GetEdgeAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + '/' + WebUtility.UrlEncode(edgeKey),
                query);
        }

        /// <summary>
        /// Gets an edge from the given graph using the edge's document-handle.
        /// GET /_api/gharial/{graph}/edge/{collection}/{edge}
        /// </summary>
        /// <typeparam name="T">The type of the edge document to deserialize to.</typeparam>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="edgeHandle">The document-handle of the edge document.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<GetEdgeResponse<T>> GetEdgeAsync<T>(
            string graphName,
            string edgeHandle,
            GetEdgeQuery query = null)
        {
            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + edgeHandle;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetEdgeResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Removes an edge from the collection.
        /// DELETE /_api/gharial/{graph}/edge/{collection}/{edge}
        /// </summary>
        /// <typeparam name="T">The type of the edge that is returned in
        /// <see cref="DeleteEdgeResponse{T}.Old"/> if requested.</typeparam>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="collectionName">The name of the edge collection the edge belongs to.</param>
        /// <param name="edgeKey">The _key attribute of the edge.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<DeleteEdgeResponse<T>> DeleteEdgeAsync<T>(
            string graphName,
            string collectionName,
            string edgeKey,
            DeleteEdgeQuery query = null)
        {
            return DeleteEdgeAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(edgeKey),
                query);

        }

        /// <summary>
        /// Removes an edge based on its document ID.
        /// </summary>
        /// <exception cref="ArgumentException">Provided document ID is invalid.</exception>
        /// <typeparam name="T">The type of the edge that is returned in
        /// <see cref="DeleteEdgeResponse{T}.Old"/> if requested.</typeparam>
        /// <param name="graphName">The name of the graph.</param>
        /// <param name="documentId">The document ID of the edge to delete.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteEdgeResponse<T>> DeleteEdgeAsync<T>(
            string graphName,
            string documentId,
            DeleteEdgeQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _transport.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteEdgeResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Gets a vertex from the given collection.
        /// GET/_api/gharial/{graph}/vertex/{collection}/{vertex}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertexKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<GetVertexResponse<T>> GetVertexAsync<T>(
            string graphName,
            string collectionName,
            string vertexKey,
            GetVertexQuery query = null)
        {
            return GetVertexAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + vertexKey,
                query);
        }

        /// <summary>
        /// Gets a vertex based on its document ID.
        /// </summary>
        /// <exception cref="ArgumentException">Provided document ID is invalid.</exception>
        /// <param name="graphName">The name of the graph to get the vertex from.</param>
        /// <param name="documentId">The document ID of the vertex to retrieve.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<GetVertexResponse<T>> GetVertexAsync<T>(
          string graphName,
          string documentId,
          GetVertexQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _transport.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetVertexResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Removes a vertex from the collection.
        /// DELETE/_api/gharial/{graph}/vertex/{collection}/{vertex}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertexKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<DeleteVertexResponse<T>> DeleteVertexAsync<T>(
            string graphName,
            string collectionName,
            string vertexKey,
            DeleteVertexQuery query = null)
        {
            return DeleteVertexAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(vertexKey),
                query);
        }

        /// <summary>
        /// Removes a vertex based on its document ID.
        /// </summary>
        /// <exception cref="ArgumentException">Provided document ID is invalid.</exception>
        /// <param name="graphName">The name of the graph to delete the vertex from.</param>
        /// <param name="documentId">The document ID of the vertex to delete.</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<DeleteVertexResponse<T>> DeleteVertexAsync<T>(
            string graphName,
            string documentId,
            DeleteVertexQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteVertexResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Updates the data of the specific vertex in the collection.
        /// PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex}
        /// </summary>
        /// <typeparam name="T">Type of the patch object</typeparam>
        /// <typeparam name="U">Type of the returned document, only applies when
        /// <see cref="PatchVertexQuery.ReturnNew"/> or <see cref="PatchVertexQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertexKey"></param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<PatchVertexResponse<U>> PatchVertexAsync<T, U>(
            string graphName,
            string collectionName,
            string vertexKey,
            T body,
            PatchVertexQuery query = null)
        {
            return PatchVertexAsync<T, U>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(vertexKey),
                body,
                query);
        }

        /// <summary>
        /// Updates the data of the specific vertex based on its document ID.
        /// </summary>
        /// <exception cref="ArgumentException">Provided document ID is invalid.</exception>
        /// <typeparam name="T">Type of the patch object</typeparam>
        /// <typeparam name="U">Type of the returned document, only applies when
        /// <see cref="PatchVertexQuery.ReturnNew"/> or <see cref="PatchVertexQuery.ReturnOld"/>
        /// are used.</typeparam>
        /// <param name="graphName">The name of the graph in which to update the vertex.</param>
        /// <param name="documentId">The document ID of the vertex to update.</param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PatchVertexResponse<U>> PatchVertexAsync<T, U>(
            string graphName,
            string documentId,
            T body,
            PatchVertexQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            var content = GetContent(body, new ApiClientSerializationOptions(false, false));
            using (var response = await _transport.PatchAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PatchVertexResponse<U>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Replaces the data of an edge in the collection.
        /// PUT /_api/gharial/{graph}/edge/{collection}/{edge}
        /// </summary>
        /// <typeparam name="T">Type of the document used for the update.</typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="edgeKey"></param>
        /// <param name="edge"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<PutEdgeResponse<T>> PutEdgeAsync<T>(
            string graphName,
            string collectionName,
            string edgeKey,
            T edge,
            PutEdgeQuery query = null)
        {
            return PutEdgeAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(edgeKey),
                edge,
                query);
        }

        /// <summary>
        /// Replaces the data of an edge based on its document ID.
        /// </summary>
        /// <exception cref="ArgumentException">Provided document ID is invalid.</exception>
        /// <typeparam name="T">Type of the document used for the update.</typeparam>
        /// <param name="graphName">The name of the graph in which to replace the edge.</param>
        /// <param name="documentId">The document ID of the edge to replace.</param>
        /// <param name="edge"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PutEdgeResponse<T>> PutEdgeAsync<T>(
            string graphName,
            string documentId,
            T edge,
            PutEdgeQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            var content = GetContent(edge, new ApiClientSerializationOptions(false,false));
            using (var response = await _transport.PutAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutEdgeResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Change one specific edge definition.
        /// This will modify all occurrences of this definition in all graphs known to your database.
        /// PUT/_api/gharial/{graph}/edge/{definition}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PutEdgeDefinitionResponse> PutEdgeDefinitionAsync(
            string graphName,
            string collectionName,
            PutEdgeDefinitionBody body,
            PutEdgeDefinitionQuery query = null)
        {
            string uriString = _graphApiPath + "/" +
                WebUtility.UrlEncode(graphName) + "/edge/" +
                WebUtility.UrlEncode(collectionName);

            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PutAsync(uriString, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutEdgeDefinitionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Updates the data of the specific edge in the collection.
        /// PATCH/_api/gharial/{graph}/edge/{collection}/{edge}
        /// </summary>
        /// <typeparam name="T">Type of the patch object used to perform a partial update of the edge document.</typeparam>
        /// <typeparam name="U">Type of the returned edge document,
        /// when <see cref="PatchEdgeQuery.ReturnOld"/> or <see cref="PatchEdgeQuery.ReturnNew"/> query params are used.</typeparam>
        /// <param name="graphName">The name of the graph in which to update the edge.</param>
        /// <param name="collectionName">The name of the edge collection.</param>
        /// <param name="edgeKey">The document key of the edge to update.</param>
        /// <param name="edge"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<PatchEdgeResponse<U>> PatchEdgeAsync<T, U>(
            string graphName,
            string collectionName,
            string edgeKey,
            T edge,
            PatchEdgeQuery query = null)
        {
            return PatchEdgeAsync<T, U>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(edgeKey),
                edge,
                query);
        }

        /// <summary>
        /// Updates the data of the specific edge based on its document ID.
        /// </summary>
        /// <typeparam name="T">Type of the patch object used to perform a partial update of the edge document.</typeparam>
        /// <typeparam name="U">Type of the returned edge document,
        /// when <see cref="PatchEdgeQuery.ReturnOld"/> or <see cref="PatchEdgeQuery.ReturnNew"/> query params are used.</typeparam>
        /// <param name="graphName">The name of the graph in which to update the edge.</param>
        /// <param name="documentId">The document ID of the edge to update.</param>
        /// <param name="edge"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PatchEdgeResponse<U>> PatchEdgeAsync<T, U>(
            string graphName,
            string documentId,
            T edge,
            PatchEdgeQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            var content = GetContent(edge, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PatchAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PatchEdgeResponse<U>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Replaces the data of a vertex in the collection.
        /// PUT/_api/gharial/{graph}/vertex/{collection}/{vertex}
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="vertex"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual Task<PutVertexResponse<T>> PutVertexAsync<T>(
            string graphName,
            string collectionName,
            string key,
            T vertex,
            PutVertexQuery query = null)
        {
            return PutVertexAsync<T>(
                graphName,
                WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(key),
                vertex,
                query);
        }

        /// <summary>
        /// Replaces the data of a vertex based on its document ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphName">The name of the graph in which to replace the vertex.</param>
        /// <param name="documentId">The document ID of the vertex to replace.</param>
        /// <param name="vertex"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PutVertexResponse<T>> PutVertexAsync<T>(
            string graphName,
            string documentId,
            T vertex,
            PutVertexQuery query = null)
        {
            ValidateDocumentId(documentId);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
               "/vertex/" + documentId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            var content = GetContent(vertex, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PutAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutVertexResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
