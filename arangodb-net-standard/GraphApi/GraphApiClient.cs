using ArangoDBNetStandard.Transport;
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

        public async Task<PostGraphResponse> PostGraph(PostGraphBody postGraphBody)
        {
            var content = GetStringContent(postGraphBody, true, true);
            using (var response = await _transport.PostAsync(_graphApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostGraphResponse>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }

        public async Task<GetGraphsResponse> GetGraphs()
        {
            using (var response = await _transport.GetAsync(_graphApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphsResponse>(stream, true, false);
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
            string uriString = _graphApiPath + "/" + graphName;
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            using (var response = await _transport.DeleteAsync(uriString))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteGraphResponse>(stream, true, false);
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
            using (var response = await _transport.GetAsync(_graphApiPath + "/" + graphName))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphResponse>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
