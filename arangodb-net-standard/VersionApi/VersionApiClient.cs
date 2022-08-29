using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.VersionApi.Models;

namespace ArangoDBNetStandard.VersionApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Version Management endpoints,
    /// implementing <see cref="IVersionApiClient"/>.
    /// </summary>
    public class VersionApiClient : ApiClientBase, IVersionApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _userApiPath = "_api/version";

        /// <summary>
        /// Creates an instance of <see cref="VersionApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public VersionApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="VersionApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public VersionApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the server version.
        /// </summary>
        /// <returns>Version information</returns>
        public virtual async Task<VersionResponse> Version()
        {
            using (var response = await _client.GetAsync(_userApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<VersionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
