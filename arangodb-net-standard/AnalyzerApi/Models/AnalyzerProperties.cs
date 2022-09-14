using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Properties of an Analyzer.
    /// See https://www.arangodb.com/docs/stable/analyzers.html#analyzer-properties
    /// </summary>
    public class AnalyzerProperties
    {
        /// <summary>
        /// Optional. When true, accented characters are preserved. 
        /// When false, accented characters are converted to 
        /// their base characters.
        /// </summary>
        public bool Accent { get; set; }

        /// <summary>
        /// A locale in the format language[_COUNTRY][.encoding][@variant] 
        /// (square brackets denote optional parts), e.g. "de.utf-8" or 
        /// "en_US.utf-8". Only UTF-8 encoding is meaningful in ArangoDB. 
        /// The locale is forwarded to ICU without checks. An invalid 
        /// locale does not prevent the creation of the Analyzer.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// The case to use when normalizing the text. Possible values:
        /// "lower" to convert to all lower-case characters
        /// "upper" to convert to all upper-case characters
        /// "none" to not change character case (default)
        /// </summary>
        public string Case { get; set; }

        /// <summary>
        /// An Analyzer is capable of removing 
        /// specified tokens from the input.
        /// It uses binary comparison to 
        /// determine if an input token should
        /// be discarded. It checks for exact 
        /// matches. If the input contains only 
        /// a substring that matches one of the 
        /// defined stopwords, then it is not discarded.
        /// Longer inputs such as prefixes of
        /// stopwords are also not discarded.
        /// </summary>
        public List<string> StopWords { get; set; }

        /// <summary>
        /// Turn Stemming ON or OFF. 
        /// If true, the analyzer stems the text, 
        /// treated as a single token, for supported languages.
        /// Stemming support is provided by Snowball,
        /// which supports the languages listed at:
        /// https://www.arangodb.com/docs/stable/analyzers.html#stemming
        /// </summary>
        public bool Stemming { get; set; }

        /// <summary>
        /// Introduced in 3.10. for minHash analyzers.
        /// An Analyzer-like definition with a type (string) 
        /// and a properties attribute (object)
        /// This is the inner analyzer to use for incoming data. 
        /// In case if omitted field or empty object falls back 
        /// to identity analyzer. 
        /// </summary>
        public Analyzer Analyzer { get; set; }

        /// <summary>
        /// Introduced in 3.10. for minHash analyzers.
        /// Specifies the size of min hash signature. 
        /// Must be greater or equal to 1. Signature 
        /// size defines probabilistic error.
        /// </summary>
        public int? NumHashes { get; set; }
    }
}