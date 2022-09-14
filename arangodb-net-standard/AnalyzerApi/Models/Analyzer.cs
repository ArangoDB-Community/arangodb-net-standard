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
        /// For rules regarding analyzer names, see
        /// https://www.arangodb.com/docs/stable/analyzers.html#analyzer-names
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the analyzer
        /// For valid analyzer types, see
        /// https://www.arangodb.com/docs/stable/analyzers.html#analyzer-types
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Properties of the analyzer
        /// used to configure the specified type
        /// </summary>
        public AnalyzerProperties Properties { get; set; }

        /// <summary>
        /// The set of features to set on 
        /// the Analyzer generated fields.
        /// Determines what term matching 
        /// capabilities will be available. 
        /// Possible features:
        /// frequency: how often a term is seen, required for PHRASE().
        /// norm: the field normalization factor.
        /// position: sequentially increasing term position,
        /// required for PHRASE(). If present then the
        /// frequency feature is also required.
        /// offset: 3.10 onwards. Enables search highlighting capabilities 
        /// for ArangoSearch Views.
        /// </summary>
        public List<string> Features { get; set; }
    }
}