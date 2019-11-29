using ArangoDBNetStandard.Transport;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;

namespace ArangoDBNetStandard.GraphApi
{
    public class GraphApiClient : ApiClientBase
    {
        private IApiClientTransport _transport;
        private readonly string _graphApiPath = "_api/gharial";

        public GraphApiClient(IApiClientTransport transport)
        {
            _transport = transport;
        }

        /// <summary>
        /// Creates a new graph in the graph module.
        /// POST /_api/gharial
        /// </summary>
        /// <param name="postGraphBody">The information of the graph to create.</param>
        /// <returns></returns>
        public async Task<PostGraphResponse> PostGraphAsync(PostGraphBody postGraphBody)
        {
            var content = GetStringContent(postGraphBody, true, true);
            using (var response = await _transport.PostAsync(_graphApiPath, content))
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
        /// Note: The <see cref="GraphResult.Name"/> property is null for <see cref="GraphApiClient.GetGraphsAsync"/> in ArangoDB 4.5.2 and below,
        /// in which case you can use <see cref="GraphResult._key"/> instead.
        /// </remarks>
        /// <returns></returns>
        public async Task<GetGraphsResponse> GetGraphsAsync()
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
        /// graphs can be deleted as well, using <see cref = "DeleteGraphQuery" ></ see >.
        /// DELETE /_api/gharial/{graph-name}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<DeleteGraphResponse> DeleteGraphAsync(string graphName, DeleteGraphQuery query = null)
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
        public async Task<GetGraphResponse> GetGraphAsync(string graphName)
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
        /// <param name="graph">The name of the graph.</param>
        /// <returns></returns>
        public async Task<GetVertexCollectionsResponse> GetVertexCollectionsAsync(string graphName)
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
        public async Task<GetGraphEdgeCollectionsResponse> GetEdgeCollectionsAsync(string graphName)
        {
            using (var response = await _transport.GetAsync(_graphApiPath + "/" + WebUtility.UrlEncode(graphName) + "/edge"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphEdgeCollectionsResponse>(stream);
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
        public async Task<PostGraphEdgeDefinitionResponse> PostEdgeDefinitionAsync(
            string graphName,
            PostGraphEdgeDefinitionBody body)
        {
            StringContent content = GetStringContent(body, true, true);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) + "/edge";

            using (var response = await _transport.PostAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostGraphEdgeDefinitionResponse>(stream);
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
        public async Task<PostVertexCollectionResponse> PostVertexCollectionAsync(
            string graphName,
            PostVertexCollectionBody body)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) + "/vertex";

            StringContent content = GetStringContent(body, true, true);

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
        /// <returns></returns>
        public async Task<PostVertexResponse<T>> PostVertexAsync<T>(
            string graphName,
            string collectionName,
            T vertex,
            PostVertexQuery query = null)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            StringContent content = GetStringContent(vertex, false, false);
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
        public async Task<DeleteEdgeDefinitionResponse> DeleteEdgeDefinitionAsync(
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
        public async Task<DeleteVertexCollectionResponse> DeleteVertexCollectionAsync(
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
        /// <returns></returns>
        public async Task<PostGraphEdgeResponse<T>> PostEdgeAsync<T>(
            string graphName,
            string collectionName,
            T edge,
            PostGraphEdgeQuery query = null)
        {
            StringContent content = GetStringContent(edge, false, false);

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
                    return DeserializeJsonFromStream<PostGraphEdgeResponse<T>>(stream);
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
        public async Task<DeleteEdgeResponse<T>> DeleteEdgeAsync<T>(
            string graphName,
            string collectionName,
            string edgeKey,
            DeleteEdgeQuery query = null)
        {
            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(edgeKey);
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

        /// Gets a vertex from the given collection.
        /// GET/_api/gharial/{graph}/vertex/{collection}/{vertex}
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertexKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<GetVertexResponse<T>> GetVertexAsync<T>(
            string graphName,
            string collectionName,
            string vertexKey,
            GetVertexQuery query = null)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName) + "/" + vertexKey;
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
        public async Task<DeleteVertexResponse<T>> DeleteVertexAsync<T>(
            string graphName,
            string collectionName,
            string vertexKey,
            DeleteVertexQuery query = null)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName) + "/" +
                WebUtility.UrlEncode(vertexKey);
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
        /// <typeparam name="T"></typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="vertexKey"></param>
        /// <param name="body"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PatchVertexResponse<U>> PatchVertexAsync<T, U>(
            string graphName,
            string collectionName,
            string vertexKey,
            T body,
            PatchVertexQuery query = null)
        {
            string uri = _graphApiPath + '/' + WebUtility.UrlEncode(graphName) +
                "/vertex/" + WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(vertexKey);
            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            StringContent content = GetStringContent(body, false, false);
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
        /// <typeparam name="T"></typeparam>
        /// <param name="graphName"></param>
        /// <param name="collectionName"></param>
        /// <param name="edgeKey"></param>
        /// <param name="edge"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PutGraphEdgeResponse<T>> PutEdgeAsync<T>(
            string graphName,
            string collectionName,
            string edgeKey,
            T edge,
            PutGraphEdgeQuery query = null)
        {
            StringContent content = GetStringContent(edge, false, false);

            string uri = _graphApiPath + "/" + WebUtility.UrlEncode(graphName) +
                "/edge/" + WebUtility.UrlEncode(collectionName) + "/" + WebUtility.UrlEncode(edgeKey);

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }
            using (var response = await _transport.PutAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutGraphEdgeResponse<T>>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
