using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for delimiter analyzer
    /// </summary>
    public class DelimiterAnalyzer : AnalyzerDefinitionPropertiesBase
    {
        /// <summary>
        /// The delimiting character(s)
        /// </summary>
        public string Delimiter { get; set; }
    }
}
