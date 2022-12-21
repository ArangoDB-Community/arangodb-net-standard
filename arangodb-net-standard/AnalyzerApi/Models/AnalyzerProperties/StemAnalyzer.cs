﻿using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for Stem analyzer
    /// </summary>
    public class StemAnalyzer : AnalyzerPropertiesBase
    {
        /// <summary>
        /// A locale in the format language, e.g. "de" or "en".
        /// The locale is forwarded to the Snowball stemmer 
        /// without checks. An invalid locale does not prevent 
        /// the creation of the Analyzer.
        /// </summary>
        public string Locale { get; set; }
    }
}