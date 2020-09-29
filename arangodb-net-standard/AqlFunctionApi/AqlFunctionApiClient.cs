using ArangoDBNetStandard.AqlFunctionApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.AqlFunctionApi
{
    /// <summary>
    /// A client to interact with ArangoDB HTTP API endpoints
    /// for AQL user functions management.
    /// </summary>
    public class AqlFunctionApiClient : ApiClientBase, IAqlFunctionApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected readonly IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _apiPath = "_api/aqlfunction";

        /// <summary>
        /// Create an instance of <see cref="AqlFunctionApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public AqlFunctionApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Create an instance of <see cref="AqlFunctionApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public AqlFunctionApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        /// <summary>
        /// Create a new AQL user function.
        /// POST /_api/aqlfunction
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<PostAqlFunctionResponse> PostAqlFunctionAsync(PostAqlFunctionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));

            using (var response = await _transport.PostAsync(_apiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostAqlFunctionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Removes an existing AQL user function or function group, identified by name.
        /// DELETE /_api/aqlfunction/{name}
        /// </summary>
        /// <param name="name">The name of the function or function group (namespace).</param>
        /// <param name="query">The query parameters of the request.</param>
        /// <returns></returns>
        public virtual async Task<DeleteAqlFunctionResponse> DeleteAqlFunctionAsync(
            string name,
            DeleteAqlFunctionQuery query = null)
        {
            string uri = _apiPath + '/' + WebUtility.UrlEncode(name);

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteAqlFunctionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Get all registered AQL user functions.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetAqlFunctionsResponse> GetAqlFunctionsAsync(GetAqlFunctionsQuery query = null)
        {
            string uri = _apiPath;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetAqlFunctionsResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
