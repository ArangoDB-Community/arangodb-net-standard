using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Represents a common response class for Analyzer API operations.
    /// </summary>
    public class GetAllAnalyzersResponse : ResponseBase
    {
        public List<Analyzer> Result { get; set; }
    }
}
