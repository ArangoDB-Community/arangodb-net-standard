using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for Classification  analyzer.
    /// (only available in the Enterprise Edition)
    /// </summary>
    public class ClassificationAnalyzer : AnalyzerDefinitionPropertiesBase
    {
        /// <summary>
        /// Required. The on-disk path to the trained fastText 
        /// supervised model. Note: if you are running this in 
        /// an ArangoDB cluster, this model must exist on every
        /// machine in the cluster.
        /// </summary>
        public string Model_Location { get; set; }

        /// <summary>
        /// Optional. The number of class labels that will be 
        /// produced per input (default: 1).
        /// </summary>
        public int? Top_K { get; set; }

        /// <summary>
        /// Optional. The probability threshold for which a 
        /// label will be assigned to an input. A fastText model
        /// produces a probability per class label, and this is 
        /// what will be filtered (default: 0.99).
        /// </summary>
        public int? Threshold { get; set; }
    }
}
