using ArangoDBNetStandard.DatabaseApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.DatabaseApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Databases endpoints,
    /// implementing <see cref="IDatabaseApiClient"/>.
    /// </summary>
    public class DatabaseApiClient : ApiClientBase, IDatabaseApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _databaseApiPath = "_api/database";

        /// <summary>
        /// Creates an instance of <see cref="DatabaseApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public DatabaseApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="DatabaseApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public DatabaseApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a new database.
        /// (Only possible from within the _system database)
        /// </summary>
        /// <param name="request">The parameters required by this endpoint.</param>
        /// <returns></returns>
        public virtual async Task<PostDatabaseResponse> PostDatabaseAsync(PostDatabaseBody request)
        {
            var content = GetContent(request, new ApiClientSerializationOptions(true, true));
            using (var response = await _client.PostAsync(_databaseApiPath, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PostDatabaseResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete a database. Dropping a database is only possible from within the _system database.
        /// The _system database itself cannot be dropped.
        /// DELETE /_api/database/{database-name}
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public virtual async Task<DeleteDatabaseResponse> DeleteDatabaseAsync(string databaseName)
        {
            using (var response = await _client.DeleteAsync(_databaseApiPath + "/" + WebUtility.UrlEncode(databaseName)).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<DeleteDatabaseResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the list of all existing databases.
        /// (Only possible from within the _system database)
        /// </summary>
        /// <remarks>
        /// You should use <see cref="GetUserDatabasesAsync"/> to fetch the list of the databases
        /// available for the current user.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<GetDatabasesResponse> GetDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetDatabasesResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the list of all databases the current user can access.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetDatabasesResponse> GetUserDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/user").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetDatabasesResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves information about the current database.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetCurrentDatabaseInfoResponse> GetCurrentDatabaseInfoAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/current").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCurrentDatabaseInfoResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }
    }
}
