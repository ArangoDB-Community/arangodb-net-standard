﻿using ArangoDBNetStandard.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.DatabaseApi
{
    public class DatabaseApiClient : ApiClientBase
    {
        private IApiClientTransport _client;
        private readonly string _databaseApiPath = "_api/database";

        public DatabaseApiClient(IApiClientTransport client)
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
            var content = GetStringContent(request, true, true);
            using (var response = await _client.PostAsync(_databaseApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostDatabaseResponse>(stream, true);
                }
                throw await GetApiErrorException(response);
            }
        }

        public async Task<DeleteDatabaseResponse> DeleteDatabaseAsync(string dbName)
        {
            using (var response = await _client.DeleteAsync(_databaseApiPath + "/" + dbName))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new DeleteDatabaseResponse((int)response.StatusCode);
                }
                var stream = await response.Content.ReadAsStreamAsync();
                var apiError = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(apiError);
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
        public async Task<ListDatabaseResponse> GetDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<ListDatabaseResponse>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Retrieves the list of all databases the current user can access.
        /// </summary>
        /// <returns></returns>
        public async Task<ListDatabaseResponse> GetUserDatabasesAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/user"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<ListDatabaseResponse>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Get the current database
        /// GET /_db/_system/_api/database/current
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentDatabaseResponse> GetCurrentDatabaseAsync()
        {
            using (var response = await _client.GetAsync(_databaseApiPath + "/current"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<CurrentDatabaseResponse>(stream, true, false);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
