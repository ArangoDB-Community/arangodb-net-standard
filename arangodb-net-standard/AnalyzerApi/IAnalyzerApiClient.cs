using ArangoDBNetStandard.AnalyzerApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.AnalyzerApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Analyzer API.
    /// </summary>
    internal interface IAnalyzerApiClient
    {
        /// <summary>
        /// Fetch the list of available Analyzer definitions.
        /// GET /_api/analyzer
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAllAnalyzersResponse> GetAllAnalyzersAsync(CancellationToken token = default);

        /// <summary>
        /// Creates a new Analyzer based on the provided definition
        /// POST /_api/analyzer
        /// </summary>
        /// <param name="body">The properties of the new analyzer.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<Analyzer> PostAnalyzerAsync(Analyzer body, CancellationToken token = default);

        /// <summary>
        /// Fetches the definition of the specified analyzer.
        /// GET /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAnalyzerResponse> GetAnalyzerAsync(string analyzerName, CancellationToken token = default);

        /// <summary>
        /// Deletes an Analyzer.
        /// DELETE /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteAnalyzerResponse> DeleteAnalyzerAsync(string analyzerName, CancellationToken token = default);
    }
}