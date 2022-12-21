using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for Segmentation analyzer
    /// </summary>
    public class SegmentationAnalyzer : AnalyzerPropertiesBase
    {
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
        /// Determines how to handle character casing
        /// Possible values are <see cref="CaseHandlingLower"/>,
        /// <see cref="CaseHandlingUpper"/>, and <see cref="CaseHandlingNone"/>
        /// </summary>
        public string Case { get; set; }
    }
}
