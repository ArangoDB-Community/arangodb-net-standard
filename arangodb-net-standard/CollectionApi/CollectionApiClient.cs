using ArangoDBNetStandard.Transport;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.CollectionApi
{
    public class CollectionApiClient : ApiClientBase
    {
        private IApiClientTransport _transport;
        private string _collectionApiPath = "_api/collection";

        public CollectionApiClient(IApiClientTransport transport)
        {
            _transport = transport;
        }

        public async Task<PostCollectionResponse> PostCollectionAsync(PostCollectionOptions options)
        {
            StringContent content = GetStringContent(options, true, true);
            using (var response = await _transport.PostAsync(_collectionApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
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
            var response = await _transport.DeleteAsync(_collectionApiPath + "/" + collectionName);
            if (response.IsSuccessStatusCode)
            {
                return new DeleteCollectionResponse((int)response.StatusCode);
            }
            var stream = await response.Content.ReadAsStreamAsync();
            var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
            throw new ApiErrorException(error);
        }

        public async Task PutCollectionTruncateAsync(string collectionName)
        {
            var response = await _transport.PutAsync(_collectionApiPath + "/" + collectionName + "/truncate", null);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            await GetApiErrorException(response);
        }
    }
}
