using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Defines an Analyzer in the database
    /// </summary>
    public class Analyzer: AnalyzerDefinition
    {
        /// <summary>
        /// Properties of the analyzer        
        /// </summary>
        public new AnalyzerProperties Properties { get; set; }
    }
}