using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.AnalyzerApi.Models;
using System;
using System.Threading;

namespace ArangoDBNetStandard.AnalyzerApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Analyzer endpoints,
    /// implementing <see cref="IAnalyzerApiClient"/>.
    /// </summary>
    public class AnalyzerApiClient : ApiClientBase, IAnalyzerApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _analyzerApiPath = "_api/analyzer";

        /// <summary>
        /// Creates an instance of <see cref="AnalyzerApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client">Transport client that the API client will use to communicate with ArangoDB</param>
        public AnalyzerApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="AnalyzerApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client">Transport client that the API client will use to communicate with ArangoDB.</param>
        /// <param name="serializer">Serializer to be used.</param>
        public AnalyzerApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Fetch the list of available Analyzer definitions.
        /// GET /_api/analyzer
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetAllAnalyzersResponse> GetAllAnalyzersAsync(CancellationToken token = default)
        {
            string uri = _analyzerApiPath;
            using (var response = await _client.GetAsync(uri, null, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetAllAnalyzersResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Creates a new Analyzer based on the provided definition
        /// POST /_api/analyzer
        /// </summary>
        /// <param name="body">The properties of the new analyzer.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<Analyzer> PostAnalyzerAsync(Analyzer body, CancellationToken token = default)
        {
            if (body == null)
            {
                throw new ArgumentException("body is required", nameof(body));
            }
            var uri = _analyzerApiPath;
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _client.PostAsync(uri, content, null, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<Analyzer>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Fetches the definition of the specified analyzer.
        /// GET /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetAnalyzerResponse> GetAnalyzerAsync(string analyzerName, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(analyzerName))
            {
                throw new ArgumentException("Analyzer name is required", nameof(analyzerName));
            }
            string uri = _analyzerApiPath + '/' + WebUtility.UrlEncode(analyzerName);
            using (var response = await _client.GetAsync(uri, null, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetAnalyzerResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Deletes an Analyzer.
        /// DELETE /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<DeleteAnalyzerResponse> DeleteAnalyzerAsync(string analyzerName, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(analyzerName))
            {
                throw new ArgumentException("Analyzer name is required", nameof(analyzerName));
            }
            string uri = _analyzerApiPath + '/' + WebUtility.UrlEncode(analyzerName);
            using (var response = await _client.DeleteAsync(uri,null,token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<DeleteAnalyzerResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }
    }
}