using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// All analyzer properties
    /// </summary>
    public class AnalyzerProperties
    {
        /// <summary>
        /// One byte is considered as one character (default)
        /// </summary>
        public const string StreamTypeBinary = "binary";

        /// <summary>
        /// One Unicode codepoint is treated as one character
        /// </summary>
        public const string StreamTypeUTF8 = "utf8";

        /// <summary>
        /// Optional. When true, accented characters are preserved. 
        /// When false, accented characters are converted to 
        /// their base characters.
        /// </summary>
        public bool? Accent { get; set; }

        /// <summary>
        /// A locale in the format language[_COUNTRY][.encoding][@variant] 
        /// (square brackets denote optional parts), e.g. "de.utf-8" or 
        /// "en_US.utf-8". Only UTF-8 encoding is meaningful in ArangoDB. 
        /// The locale is forwarded to ICU without checks. An invalid 
        /// locale does not prevent the creation of the Analyzer.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Determines how to handle character casing
        /// Possible values are <see cref="CaseHandlingLower"/>,
        /// <see cref="CaseHandlingUpper"/>, and <see cref="CaseHandlingNone"/>
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
        public bool? Stemming { get; set; }

        /// <summary>
        /// Introduced in 3.10 for minHash analyzers.
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
        /// Must be greater or equal to 1. 
        /// The signature size defines the probalistic 
        /// error (err = rsqrt(numHashes)). For an error
        /// amount that does not exceed 5% (0.05), use a
        /// size of 1 / (0.05 * 0.05) = 400.
        /// </summary>
        public int? NumHashes { get; set; }

        /// <summary>
        /// Introduced in 3.10. The on-disk path to 
        /// the trained fastText supervised model.
        /// Note: if you are running this in an 
        /// ArangoDB cluster, this model must 
        /// exist on every machine in the cluster.
        /// Required for classification and 
        /// nearest_neighbors  analyzers. 
        /// </summary>
        public string Model_Location { get; set; }

        /// <summary>
        /// Introduced in 3.10. The number of class
        /// labels that will be produced per input
        /// (default: 1).
        /// Optional for classification and 
        /// nearest_neighbors analyzers.
        /// </summary>
        public int? Top_K { get; set; }

        /// <summary>
        /// Introduced in 3.10. The probability 
        /// threshold for which a label will be 
        /// assigned to an input. A fastText model
        /// produces a probability per class label, 
        /// and this is what will be filtered 
        /// (default: 0.99).
        /// Optional for Classification analyzers.
        /// </summary>
        public decimal? Threshold { get; set; }

        /// <summary>
        /// Unsigned integer for the minimum n-gram length
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// Unsigned integer for the maximum n-gram length
        /// </summary>
        public int? Max { get; set; }

        /// <summary>
        /// When true: include the original value as well.
        /// When false: produce the n-grams based on <see cref="Min"/> 
        /// and <see cref="Max"/> only. 
        /// </summary>
        public bool? PreserveOriginal { get; set; }

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

        /// <summary>
        /// Convert emitted tokens to strings. (default)
        /// </summary>
        public const string ReturnTypeString = "string";

        /// <summary>
        /// Convert emitted tokens to numbers
        /// </summary>
        public const string ReturnTypeNumber = "number";

        /// <summary>
        /// Convert emitted tokens to booleans
        /// </summary>
        public const string ReturnTypeBool = "bool";

        /// <summary>
        /// AQL query to be executed
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// When true: set the position to 0 for all members
        /// of the query result array.
        /// When false: (default): set the position corresponding
        /// to the index of the result array member
        /// </summary>
        public bool? CollapsePositions { get; set; }

        /// <summary>
        /// When true: (default): treat null like an empty string.
        /// When false: discard nulls from View index. Can be used
        /// for index filtering (i.e. make your query return null 
        /// for unwanted data). Note that empty results are always
        /// discarded.
        /// </summary>
        public bool? KeepNull { get; set; }

        /// <summary>
        /// A number between 1 and 1000 (default = 1) that determines 
        /// the batch size for reading data from the query. In general, 
        /// a single token is expected to be returned. However, if the 
        /// query is expected to return many results, then increasing 
        /// <see cref="BatchSize"/> trades memory for performance.
        /// </summary>
        public int? BatchSize { get; set; }

        /// <summary>
        /// Memory limit for query execution in bytes. 
        /// (default is 1048576 = 1Mb) 
        /// Maximum is 33554432U (32Mb)
        /// </summary>
        public int? MemoryLimit { get; set; }

        /// <summary>
        /// The Data type of the returned tokens. If the indicated 
        /// type does not match the actual type then an implicit 
        /// type conversion is applied. For possible values, see
        /// <see cref="ReturnTypeBool"/>, <see cref="ReturnTypeNumber"/>
        /// and <see cref="ReturnTypeString"/>
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// The delimiting character(s)
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// Index all GeoJSON geometry types (Point, Polygon etc.)
        /// (default).
        /// </summary>
        public const string TypeShape = "shape";

        /// <summary>
        /// Compute and only index the centroid of the input 
        /// geometry
        /// </summary>
        public const string TypeCentroid = "centroid";

        /// <summary>
        /// Only index GeoJSON objects of type Point, ignore all 
        /// other geometry types
        /// </summary>
        public const string TypePoint = "point";

        /// <summary>
        /// Determines the type of indexing to use.
        /// Possible values are <see cref="TypeCentroid"/>, 
        /// <see cref="TypeShape"/> and <see cref="TypePoint"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Options for fine-tuning geo queries. 
        /// These options should generally remain unchanged.
        /// </summary>
        public GeoJSONAnalyzerOptions Options { get; set; }

        /// <summary>
        /// A list of strings that describes the attribute path 
        /// of the latitude value relative to the field for 
        /// which the Analyzer is defined in the View.
        /// </summary>
        public List<string> Latitude { get; set; }

        /// <summary>
        /// A list of  strings that describes the attribute path
        /// of the longitude value relative to the field for 
        /// which the Analyzer is defined in the View.
        /// </summary>
        public List<string> Longitude { get; set; }

        /// <summary>
        /// Convert to all lower-case characters
        /// </summary>
        public const string CaseHandlingLower = "lower";

        /// <summary>
        /// Convert to all upper-case characters
        /// </summary>
        public const string CaseHandlingUpper = "upper";

        /// <summary>
        /// Do not change character case (default)
        /// </summary>
        public const string CaseHandlingNone = "none";

        /// <summary>
        /// An array of Analyzer objects to use for the pipeline.
        /// Analyzers of types geopoint and geojson cannot be used
        /// in pipelines and will make the creation fail. 
        /// These Analyzers require additional postprocessing and 
        /// can only be applied to document fields directly.
        /// </summary>
        public List<Analyzer> Pipeline { get; set; }

        /// <summary>
        /// Return all tokens
        /// </summary>
        public const string BreakTypeAll = "all";

        /// <summary>
        /// Return tokens composed of alphanumeric characters only (default).
        /// </summary>
        public const string BreakTypeAlpha = "alpha";

        /// <summary>
        /// Return tokens composed of non-whitespace characters only. 
        /// Note that the list of whitespace characters does not include line breaks: 
        /// U+0009 Character Tabulation, U+0020 Space, U+0085 Next Line,
        /// U+00A0 No-break Space, U+1680 Ogham Space Mark, U+2000 En Quad,
        /// U+2028 Line Separator, U+202F Narrow No-break Space,
        /// U+205F Medium Mathematical Space, and U+3000 Ideographic Space.
        /// </summary>
        public const string BreakTypeGraphic = "graphic";

        /// <summary>
        /// Determines how to break up the input text.
        /// Possible values are <see cref="BreakTypeAll"/>, <see cref="BreakTypeAlpha"/>,
        /// and <see cref="BreakTypeGraphic"/>
        /// </summary>
        public string Break { get; set; }

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
        public bool? Hex { get; set; }

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
