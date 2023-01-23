using ArangoDBNetStandard.CollectionApi.Models.Figures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Attribute paths specifying options for index fields.
    /// </summary>
    public class InvertedIndexField
    {
        /// <summary>
        /// Required. An attribute path. The . character denotes 
        /// sub-attributes. You can expand one array attribute 
        /// with [*].
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional. The name of an Analyzer to use for this field.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Optional. A list of Analyzer features to use for this field. 
        /// You can set this option to overwrite what features 
        /// are enabled for the analyzer. For possible values,
        /// <see cref="AnalyzerApi.Models.AnalyzerFeatures"/>
        /// </summary>
        public IEnumerable<string> Features { get; set; }

        /// <summary>
        /// Optional. This option only applies if you use the 
        /// inverted index in a search-alias Views.
        /// If set to true, then all sub-attributes of this field 
        /// are indexed, excluding any sub-attributes that are 
        /// configured separately by other elements in the fields 
        /// array(and their sub-attributes). The analyzer and 
        /// features properties apply to the sub-attributes.
        /// If set to false, then sub-attributes are ignored.
        /// Default: the value defined by the top-level
        /// <see cref="PostInvertedIndexBody.IncludeAllFields"/> option.
        /// </summary>
        public bool? IncludeAllFields { get; set; }

        /// <summary>
        /// Optional. This option only applies if you use the inverted index 
        /// in a search-alias Views.
        /// You can set the option to true to get the same behavior 
        /// as with arangosearch Views regarding the indexing of array 
        /// values for this field.If enabled, both, array and primitive 
        /// values(strings, numbers, etc.) are accepted.Every element 
        /// of an array is indexed according to the 
        /// <see cref="TrackListPositions"/> option.
        /// If set to false, it depends on the attribute path. If it 
        /// explicitly expands an array ([*]), then the elements are 
        /// indexed separately. Otherwise, the array is indexed as a 
        /// whole, but only geopoint and aql Analyzers accept array 
        /// inputs.You cannot use an array expansion if 
        /// <see cref="SearchField"/> is enabled.
        /// Default: the value defined by the top-level 
        /// <see cref="PostInvertedIndexBody.SearchField"/> option.
        /// </summary>
        public bool? SearchField { get; set; }

        /// <summary>
        /// Optional. This option only applies if you use the inverted 
        /// index in a search-alias Views.
        /// If set to true, then track the value position in arrays for 
        /// array values.For example, when querying a document like 
        /// { attr: [ "valueX", "valueY", "valueZ" ] }, you need to specify 
        /// the array element, e.g.doc.attr[1] == "valueY".
        /// If set to false, all values in an array are treated as equal 
        /// alternatives.You don’t specify an array element in queries, 
        /// e.g.doc.attr == "valueY", and all elements are searched for a match.
        /// Default: the value defined by the top-level
        /// <see cref="PostInvertedIndexBody.TrackListPositions"/> option.
        /// </summary>
        public bool? TrackListPositions { get; set; }

        /// <summary>
        /// Optional. Enable this option to always cache the field normalization
        /// values in memory for this specific field. This can improve 
        /// the performance of scoring and ranking queries.
        /// Normalization values are computed for fields which are processed
        /// with Analyzers that have the "norm" feature enabled.
        /// These values are used to score fairer if the same tokens occur
        /// repeatedly, to emphasize these documents less.
        /// Default: the value defined by the top-level 
        /// <see cref="PostInvertedIndexBody.Cache"/> option.
        /// </summary>
        public bool? Cache { get; set; }

        /// <summary>
        /// Optional. Index the specified sub-objects that are stored in an array. 
        /// Other than with the fields property, the values get indexed in a way
        /// that lets you query for co-occurring values. For example, you can 
        /// search the sub-objects and all the conditions need to be met by a 
        /// single sub-object instead of across all of them.
        /// This property is available in the Enterprise Edition only.
        /// </summary>
        public IEnumerable<InvertedIndexField> Nested { get; set; }
    }
}