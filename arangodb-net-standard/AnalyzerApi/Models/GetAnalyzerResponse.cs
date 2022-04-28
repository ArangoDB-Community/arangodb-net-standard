using System.Net;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Response from <see cref="IAnalyzerApiClient.GetAnalyzerAsync(string)"/>
    /// </summary>
    public class GetAnalyzerResponse : Analyzer
    {
        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}