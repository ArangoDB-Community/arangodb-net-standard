using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for text analyzer
    /// </summary>
    public class TextAnalyzer
    {
        /// <summary>
        /// Convert to all lower-case characters
        /// </summary>
        public static string CaseHandlingLower = "lower";

        /// <summary>
        /// Convert to all upper-case characters
        /// </summary>
        public static string CaseHandlingUpper = "upper";

        /// <summary>
        /// Do not change character case (default)
        /// </summary>
        public static string CaseHandlingNone = "none";

        /// <summary>
        /// A locale in the format language[_COUNTRY] 
        /// (square brackets denote optional parts), 
        /// e.g. "de" or "en_US". The locale is forwarded 
        /// to ICU without checks. An invalid locale does 
        /// not prevent the creation of the Analyzer.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// When true: preserves accented characters (default).
        /// When false: converts accented characters to their 
        /// base characters. 
        /// </summary>
        public bool? Accent { get; set; }

        /// <summary>
        /// Determines how to handle character casing
        /// Possible values are <see cref="CaseHandlingLower"/>,
        /// <see cref="CaseHandlingUpper"/>, and <see cref="CaseHandlingNone"/>
        /// </summary>
        public string Case { get; set; }

        /// <summary>
        /// When true: apply stemming on returned words (default).
        /// When false: leave the tokenized words as-is.
        /// </summary>
        public bool? Stemming { get; set; }

        /// <summary>
        /// If present, then edge n-grams are generated for 
        /// each token (word). That is, the start of the 
        /// n-gram is anchored to the beginning of the token,
        /// whereas the ngram Analyzer would produce all possible
        /// substrings from a single input token (within the 
        /// defined length restrictions). Edge n-grams can be 
        /// used to cover word-based auto-completion queries 
        /// with an index, for which you should set the following 
        /// other options: <see cref="Accent"/> = false, 
        /// <see cref="Case"/> = <see cref="CaseHandlingLower"/>
        /// and most importantly <see cref="Stemming"/> = false. 
        /// </summary>
        public TextAnalyzerEdgeNgram EdgeNgram { get; set; }

        /// <summary>
        /// A list of strings with words to omit from result. 
        /// Default: load words from <see cref="StopwordsPath"/>. 
        /// To disable stop-word filtering provide an empty list. 
        /// If both <see cref="Stopwords"/> and <see cref="StopwordsPath"/>
        /// are provided then both word sources are combined.
        /// </summary>
        public List<string> Stopwords { get; set; }

        /// <summary>
        /// A path with a language sub-directory (e.g. en for a locale en_US) 
        /// containing files with words to omit. Each word has to be on a
        /// separate line. Everything after the first whitespace character 
        /// on a line will be ignored and can be used for comments. The files
        /// can be named arbitrarily and have any file extension (or none).
        /// Default: if no path is provided then the value of the environment 
        /// variable IRESEARCH_TEXT_STOPWORD_PATH is used to determine the path,
        /// or if it is undefined then the current working directory is assumed.
        /// If the stopwords attribute is provided then no stop-words are loaded
        /// from files, unless an explicit <see cref="StopwordsPath"/> is also provided.
        /// Note that if the <see cref="StopwordsPath"/> can not be accessed,
        /// is missing language sub-directories or has no files for a language 
        /// required by an Analyzer, then the creation of a new Analyzer 
        /// is refused. If such an issue is discovered for an existing Analyzer 
        /// during startup then the server will abort with a fatal error.
        /// </summary>
        public string StopwordsPath { get; set; }
    }
}
