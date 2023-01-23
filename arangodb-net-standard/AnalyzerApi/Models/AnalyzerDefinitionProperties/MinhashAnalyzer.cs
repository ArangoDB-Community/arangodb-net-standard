using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for Minhash analyzer.
    /// (only available in the Enterprise Edition)
    /// </summary>
    public class MinhashAnalyzer : AnalyzerDefinitionPropertiesBase
    {
        /// <summary>
        /// An Analyzer object.
        /// </summary>
        public Analyzer Analyzer { get; set; }

        /// <summary>
        /// The size of the MinHash signature. Must be greater or 
        /// equal to 1. The signature size defines the probalistic 
        /// error (err = rsqrt(numHashes)). For an error amount 
        /// that does not exceed 5% (0.05),
        /// use a size of 1 / (0.05 * 0.05) = 400.
        /// </summary>
        public int NumHashes { get; set; }
    }
}
