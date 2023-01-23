using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for Stopwords analyzer
    /// </summary>
    public class StopwordsAnalyzer : AnalyzerDefinitionPropertiesBase
    {
        /// <summary>
        /// A list of strings that describe the tokens to be discarded. 
        /// The interpretation of each string depends on the value of
        /// the <see cref="Hex"/> property.
        /// </summary>
        public List<string> Stopwords { get; set; }

        /// <summary>
        /// If false (default), then each string in <see cref="Stopwords"/>
        /// is used verbatim. If true, then the strings need to be hex-encoded.
        /// This allows for removing tokens that contain non-printable characters.
        /// </summary>
        public bool Hex { get; set; }
    }
}
