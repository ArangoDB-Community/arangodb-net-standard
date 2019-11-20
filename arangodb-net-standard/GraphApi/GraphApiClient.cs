using ArangoDBNetStandard.Transport;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
        public async Task<GetVertexCollectionsResponse> GetVertexCollections(string graphName)
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
        public async Task<GetGraphEdgeCollectionsResponse> GetGraphEdgeCollectionsAsync(string graphName)
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
        /// each from and to vertex collections.An edge definition can only
        /// be added if this definition is either not used in any other graph, or
        /// it is used with exactly the same definition. It is not possible to
        /// store a definition “e” from “v1” to “v2” in the one graph, and “e”
        /// from “v2” to “v1” in the other graph.
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<PostGraphEdgeResponse> PostGraphEdgeDefinitionAsync(string graphName, PostGraphEdgeBody body)
        {
            StringContent content = GetStringContent(body, true, true);
            using (var response = await _transport.PostAsync(_graphApiPath + "/" + WebUtility.UrlEncode(graphName) + "/edge", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostGraphEdgeResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
