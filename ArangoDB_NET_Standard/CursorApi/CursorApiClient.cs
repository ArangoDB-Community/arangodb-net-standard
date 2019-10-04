using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB_NET_Standard.CursorApi
{
    /// <summary>
    /// ArangoDB Cursor API.
    /// </summary>
    public class CursorApiClient: ApiClientBase
    {
        private readonly string _cursorApiPath = "_api/cursor";
        private HttpClient _client;

        /// <summary>
        /// Create a new <see cref="CursorApi"/>.
        /// </summary>
        /// <param name="client">Set base path and appropriate auth headers on the passed in client.</param>
        public CursorApiClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Execute an AQL query, creating a cursor which can be used to page query results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="bindVars"></param>
        /// <param name="options"></param>
        /// <param name="count"></param>
        /// <param name="batchSize"></param>
        /// <param name="cache"></param>
        /// <param name="memoryLimit"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        public async Task<CursorResponse<T>> PostCursorAsync<T>(
                string query,
                Dictionary<string, object> bindVars = null,
                PostCursorOptions options = null,
                bool? count = null,
                long? batchSize = null,
                bool? cache = null,
                long? memoryLimit = null,
                int? ttl = null)
        {
            return await PostCursorAsync<T>(new PostCursorRequest
            {
                Query = query,
                BindVars = bindVars,
                Options = options,
                Count = count,
                BatchSize = batchSize,
                Cache = cache,
                MemoryLimit = memoryLimit,
                Ttl = ttl
            });
        }

        public async Task<CursorResponse<T>> PostCursorAsync<T>(PostCursorRequest cursorRequest)
        {
            var content = GetStringContent(cursorRequest, true, true);
            using (var response = await _client.PostAsync(_cursorApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<CursorResponse<T>>(stream);
                }
                ApiErrorResponse error = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(error);
            }
        }

        public async Task<DeleteCursorResponse> DeleteCursorAsync(string cursorId)
        {
            using (var response = await _client.DeleteAsync(_cursorApiPath + "/" + cursorId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new DeleteCursorResponse((int)response.StatusCode);
                }
                var stream = await response.Content.ReadAsStreamAsync();
                ApiErrorResponse error = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(error);
            }
        }

        public async Task<PutCursorResponse<T>> PutCursorAsync<T>(string cursorId)
        {
            using (var response = await _client.PutAsync(_cursorApiPath + "/" + cursorId, new StringContent(string.Empty)))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PutCursorResponse<T>>(stream);
                }
                ApiErrorResponse error = DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false);
                throw new ApiErrorException(error);
            }
        }
    }
}
