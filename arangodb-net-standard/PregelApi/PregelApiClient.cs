using ArangoDBNetStandard.ViewApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using ArangoDBNetStandard.PregelApi.Models;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;
using System;
using System.Security.Cryptography;

namespace ArangoDBNetStandard.PregelApi
{
    /// <summary>
    /// A client to interact with ArangoDB HTTP API endpoints
    /// for Pregel jobs management.
    /// </summary>
    public class PregelApiClient : ApiClientBase, IPregelApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected readonly IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _apiPath = "_api/control_pregel";

        /// <summary>
        /// Create an instance of <see cref="PregelApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public PregelApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Create an instance of <see cref="PregelApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public PregelApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        /// <summary>
        /// Start the execution of a Pregel algorithm.
        /// POST /_api/control_pregel
        /// </summary>
        /// <remarks>
        /// To start an execution you need to specify the
        /// algorithm name and a named graph (SmartGraph in cluster).
        /// Alternatively you can specify the vertex and edge collections.
        /// Additionally you can specify custom parameters which 
        /// vary for each algorithm
        /// </remarks>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns>The ID of the newly started job.</returns>
        public virtual async Task<string> PostStartJobAsync(PostStartJobBody body, CancellationToken token = default)
        {
            string uri = _apiPath;
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PostAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<string>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the status of a Pregel job execution.
        /// GET /_api/control_pregel/{id}
        /// </summary>
        /// <param name="jobId">The ID of the job.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PregelJobStatus> GetJobStatusAsync(string jobId, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(nameof(jobId));
            }
            string uri = _apiPath + '/' + jobId;
            using (var response = await _transport.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PregelJobStatus>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the overview of currently running Pregel jobs.
        /// GET /_api/control_pregel
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns>
        /// Returns a list of currently running and recently
        /// finished Pregel jobs without retrieving their results. 
        /// </returns>
        public virtual async Task<List<PregelJobStatus>> GetAllRunningJobsAsync(CancellationToken token = default)
        {
            string uri = _apiPath;
            using (var response = await _transport.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<List<PregelJobStatus>>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Cancel an ongoing Pregel execution.
        /// DELETE /_api/control_pregel/{id}
        /// </summary>
        /// <remarks>
        /// Cancel an execution which is still running, and 
        /// discard any intermediate results. This will immediately 
        /// free all memory taken up by the execution, and will 
        /// make you lose all intermediary data.
        /// For more information see https://www.arangodb.com/docs/stable/http/pregel.html#cancel-pregel-job-execution
        /// </remarks>
        /// <param name="jobId">The ID of the job.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<string> DeleteJobAsync(string jobId, CancellationToken token = default)
        {
            string uri = _apiPath + '/' + jobId;
            using (var response = await _transport.DeleteAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<string>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }
    }
}