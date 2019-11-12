using ArangoDBNetStandard.Transport;
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

        public async Task<PostGraphResult> PostGraph(PostGraphBody postGraphBody)
        {
            var content = GetStringContent(postGraphBody, true, true);
            using (var response = await _transport.PostAsync(_graphApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostGraphResult>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }

        public async Task<GetGraphsResult> GetGraphs()
        {
            using (var response = await _transport.GetAsync(_graphApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetGraphsResult>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
