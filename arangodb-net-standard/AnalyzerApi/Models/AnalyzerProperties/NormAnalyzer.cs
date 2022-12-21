using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for norm analyzer
    /// </summary>
    public class NormAnalyzer : AnalyzerPropertiesBase
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
    }
}
