using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for nearest_neighbors analyzer.
    /// </summary>
    public class NearestNeighborsAnalyzer : AnalyzerDefinitionPropertiesBase
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
    }
}
