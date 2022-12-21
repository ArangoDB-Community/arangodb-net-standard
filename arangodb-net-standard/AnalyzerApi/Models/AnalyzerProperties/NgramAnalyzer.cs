using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for ngram analyzer
    /// </summary>
    public class NgramAnalyzer
    {
        /// <summary>
        /// One byte is considered as one character (default)
        /// </summary>
        public static string StreamTypeBinary = "binary";

        /// <summary>
        /// One Unicode codepoint is treated as one character
        /// </summary>
        public static string StreamTypeUTF8 = "utf8";

        /// <summary>
        /// Unsigned integer for the minimum n-gram length
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Unsigned integer for the maximum n-gram length
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// When true: include the original value as well.
        /// When false: produce the n-grams based on <see cref="Min"/> 
        /// and <see cref="Max"/> only. 
        /// </summary>
        public bool PreserveOriginal { get; set; }

        /// <summary>
        /// This value will be prepended to n-grams which
        /// include the beginning of the input. Can be used
        /// for matching prefixes. Choose a character or 
        /// sequence as marker which does not occur in the input.
        /// </summary>
        public string StartMarker { get; set; }

        /// <summary>
        /// This value will be appended to n-grams which 
        /// include the end of the input. Can be used for 
        /// matching suffixes. Choose a character or sequence
        /// as marker which does not occur in the input.
        /// </summary>
        public string EndMarker { get; set; }

        /// <summary>
        /// Type of the input stream.
        /// Possible values are <see cref="StreamTypeBinary"/>
        /// and <see cref="StreamTypeUTF8"/>
        /// </summary>
        public string StreamType { get; set; }
    }
}
