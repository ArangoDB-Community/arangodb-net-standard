using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Defines an Analyzer in the database
    /// </summary>
    public class Analyzer
    {
        /// <summary>
        /// Name of the analyzer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the analyzer
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Properties of the analyzer
        /// used to configure the specified type
        /// </summary>
        public AnalyzerProperties Properties { get; set; }

        /// <summary>
        /// The set of features to set on 
        /// the Analyzer generated fields
        /// </summary>
        public List<string> Features { get; set; }
    }
}