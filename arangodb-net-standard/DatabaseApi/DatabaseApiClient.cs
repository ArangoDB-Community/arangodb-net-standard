using ArangoDBNetStandard.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.DatabaseApi
{
    public class DatabaseApiClient: ApiClientBase
    {
        private IApiClientTransport _client;
        private readonly string _databaseApiPath = "_api/database";

        public DatabaseApiClient(IApiClientTransport client)
        {
            _client = client;
        }

        public async Task<PostDatabaseResult> PostDatabaseAsync(PostDatabaseRequest request)
        {
            var content = GetStringContent(request, true, true);
            using (var response = await _client.PostAsync(_databaseApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new PostDatabaseResult((int)response.StatusCode);
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var apiError = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(apiError);
            }
        }

        public async Task<DeleteDatabaseResult> DeleteDatabaseAsync(string dbName)
        {
            using (var response = await _client.DeleteAsync(_databaseApiPath + "/" + dbName))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new DeleteDatabaseResult((int)response.StatusCode);
                }
                var stream = await response.Content.ReadAsStreamAsync();
                var apiError = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(apiError);
            }
        }
    }
}
