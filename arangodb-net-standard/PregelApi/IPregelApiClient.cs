using ArangoDBNetStandard.PregelApi.Models;
using ArangoDBNetStandard.ViewApi.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ArangoDBNetStandard.PregelApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB API for Pregel 
    /// (Distributed Iterative Graph Processing).
    /// </summary>
    public interface IPregelApiClient
    {
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
        Task<string> PostStartJobAsync(
            PostStartJobBody body,
            CancellationToken token = default);

        /// <summary>
        /// Get the status of a Pregel job execution.
        /// GET /_api/control_pregel/{id}
        /// </summary>
        /// <param name="jobId">The ID of the job.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PregelJobStatus> GetJobStatusAsync(
            string jobId,
            CancellationToken token = default);

        /// <summary>
        /// Get the overview of currently running Pregel jobs.
        /// GET /_api/control_pregel
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns>
        /// Returns a list of currently running and recently
        /// finished Pregel jobs without retrieving their results. 
        /// </returns>
        Task<List<PregelJobStatus>> GetAllRunningJobsAsync(
            CancellationToken token = default);

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
        Task<string> DeleteJobAsync(
            string jobId,
            CancellationToken token = default);
    }
}