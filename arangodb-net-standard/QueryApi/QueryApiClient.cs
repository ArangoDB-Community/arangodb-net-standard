using ArangoDBNetStandard.QueryApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.QueryApi
{
    /// <summary>
    /// Provides access to ArangoDB query API.
    /// </summary>
    public class QueryApiClient : ApiClientBase, IQueryApiClient
    {
        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _cursorApiPath = "_api/cursor";

        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// Creates an instance of <see cref="QueryApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public QueryApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="QueryApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public QueryApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Posts a single query that returns basic statistics about the result.
        /// </summary>
        /// <param name="postQueryBody">Object encapsulating options and parameters of the query.</param>
        /// <returns></returns>
        public async Task<ExecuteNonQueryResponse> PostExecuteNonQueryAsync(PostQueryBody postQueryBody)
        {
            var content = await GetContentAsync(postQueryBody, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            
            using (var response = await _client.PostAsync(_cursorApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return await DeserializeJsonFromStreamAsync<ExecuteNonQueryResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }
    }
}
