using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerDefinitionProperties
{
    /// <summary>
    /// Properties for Pipeline analyzer
    /// </summary>
    public class PipelineAnalyzer : AnalyzerDefinitionPropertiesBase
    {
        /// <summary>
        /// An array of Analyzer objects to use for the pipeline.
        /// Analyzers of types geopoint and geojson cannot be used
        /// in pipelines and will make the creation fail. 
        /// These Analyzers require additional postprocessing and 
        /// can only be applied to document fields directly.
        /// </summary>
        public List<Analyzer> Pipeline { get; set; }
    }
}
