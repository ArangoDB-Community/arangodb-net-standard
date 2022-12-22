using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Possible features for Analyzers
    /// </summary>
    public static class AnalyzerFeatures
    {
        /// <summary>
        /// Track how often a term occurs. Required for 
        /// PHRASE(), NGRAM_MATCH(), BM25(), TFIDF(), 
        /// and OFFSET_INFO()
        /// </summary>
        public const string Frequency = "frequency";

        /// <summary>
        /// Write the field length normalization factor 
        /// that is used to score repeated terms fairer. 
        /// Required for BM25() (except BM15) and TFIDF() 
        /// (if called with normalization enabled).
        /// </summary>
        public const string Norm = "norm";

        /// <summary>
        /// Enumerate the tokens for position-dependent
        /// queries. Required for PHRASE(), NGRAM_MATCH(), 
        /// and OFFSET_INFO(). If present, then the frequency
        /// feature is also required.
        /// </summary>
        public const string Position = "position";

        /// <summary>
        /// Enable search highlighting capabilities 
        /// (Enterprise Edition only). 
        /// Required for OFFSET_INFO(). If present, then 
        /// the position and frequency features are also
        /// required.
        /// </summary>
        public const string Offset = "offset";
    }
}
