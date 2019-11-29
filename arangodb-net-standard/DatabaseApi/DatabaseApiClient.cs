using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.DatabaseApi
{
    public class DatabaseApiClient : ApiClientBase
    {
        private IApiClientTransport _client;
        private readonly string _databaseApiPath = "_api/database";

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
        public async Task<PostDatabaseResponse> PostDatabaseAsync(PostDatabaseBody request)
        {
            var content = GetContent(request, true, true);
            using (var response = await _client.PostAsync(_databaseApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDatabaseResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Delete a database. Dropping a database is only possible from within the _system database.
        /// The _system database itself cannot be dropped.
        /// DELETE /_api/database/{database-name}
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public async Task<DeleteDatabaseResponse> DeleteDatabaseAsync(string databaseName)
        {
            using (var response = await _client.DeleteAsync(_databaseApiPath + "/" + WebUtility.UrlEncode(databaseName)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteDatabaseResponse>(stream);
                }
                throw await GetApiErrorException(response);
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
        public async Task<GetDatabasesResponse> GetDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetDatabasesResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Retrieves the list of all databases the current user can access.
        /// </summary>
        /// <returns></returns>
        public async Task<GetDatabasesResponse> GetUserDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/user"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetDatabasesResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Retrieves information about the current database.
        /// </summary>
        /// <returns></returns>
        public async Task<GetCurrentDatabaseInfoResponse> GetCurrentDatabaseInfoAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/current"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCurrentDatabaseInfoResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
