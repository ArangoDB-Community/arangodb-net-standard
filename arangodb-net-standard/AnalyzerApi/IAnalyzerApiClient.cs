using ArangoDBNetStandard.AnalyzerApi.Models;
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
        /// <returns></returns>
        Task<GetAllAnalyzersResponse> GetAllAnalyzersAsync();

        /// <summary>
        /// Creates a new Analyzer based on the provided definition
        /// POST /_api/analyzer
        /// </summary>
        /// <param name="body">The properties of the new analyzer.</param>
        /// <returns></returns>
        Task<Analyzer> PostAnalyzerAsync(Analyzer body);

        /// <summary>
        /// Fetches the definition of the specified analyzer.
        /// GET /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <returns></returns>
        Task<GetAnalyzerResponse> GetAnalyzerAsync(string analyzerName);

        /// <summary>
        /// Deletes an Analyzer.
        /// DELETE /_api/analyzer/{analyzer-name}
        /// </summary>
        /// <param name="analyzerName">The name of the analyzer</param>
        /// <returns></returns>
        Task<DeleteAnalyzerResponse> DeleteIndexAsync(string analyzerName);
    }
}
