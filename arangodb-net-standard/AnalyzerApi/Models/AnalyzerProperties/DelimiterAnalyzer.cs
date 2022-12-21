using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for delimiter analyzer
    /// </summary>
    public class DelimiterAnalyzer : AnalyzerPropertiesBase
    {
        /// <summary>
        /// The delimiting character(s)
        /// </summary>
        public string Delimiter { get; set; }
    }
}
