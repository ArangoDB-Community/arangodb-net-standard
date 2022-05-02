using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Response from <see cref="IAnalyzerApiClient.GetAllAnalyzersAsync"/>
    /// </summary>
    public class GetAllAnalyzersResponse : ResponseBase
    {
        /// <summary>
        /// List of analyzers
        /// </summary>
        public List<Analyzer> Result { get; set; }
    }
}
