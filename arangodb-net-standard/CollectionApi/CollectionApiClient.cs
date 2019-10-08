using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.CollectionApi
{
    public class CollectionApiClient : ApiClientBase
    {
        private HttpClient _client;
        private string _collectionApiPath = "_api/collection";

        public CollectionApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<PostCollectionResponse> PostCollectionAsync(PostCollectionOptions options)
        {
            StringContent content = GetStringContent(options, true, true);
            using (var response = await _client.PostAsync(_collectionApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var str = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostCollectionResponse>(stream, true, false);
                }
                throw new ApiErrorException(
                    DeserializeJsonFromStream<ApiErrorResponse>(stream, true, false));
            }
        }

        public async Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName)
        {
            var response = await _client.DeleteAsync(_collectionApiPath + "/" + collectionName);
            if (response.IsSuccessStatusCode)
            {
                return new DeleteCollectionResponse((int)response.StatusCode);
            }
            var stream = await response.Content.ReadAsStreamAsync();
            var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
            throw new ApiErrorException(error);
        }
    }
}
