using ArangoDBNetStandard.CollectionApi.Models.Figures;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Linq;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body for <see cref="IIndexApiClient.PostInvertedIndexAsync(PostIndexQuery, PostInvertedIndexBody, System.Threading.CancellationToken)"/>
    /// </summary>
    public class PostInvertedIndexBody : PostIndexBody, IInvertedIndex
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PostInvertedIndexBody()
        {
            Type = IndexTypes.Inverted;
        }

        /// <summary>
        /// An list of attribute paths specifying options 
        /// for the fields (with the attribute path 
        /// in the name property)
        /// </summary>
        public new IEnumerable<InvertedIndexField> Fields { get; set; }

        /// <summary>
        /// Optional. This option only applies if you use the
        /// inverted index in a search-alias Views.
        /// You can set the option to true to get the same behavior
        /// as with arangosearch Views regarding the indexing of array
        /// values as the default. If enabled, both, array and primitive 
        /// values(strings, numbers, etc.) are accepted. Every element 
        /// of an array is indexed according to the <see cref="TrackListPositions"/> 
        /// option. If set to false, it depends on the attribute path. 
        /// If it explicitly expands an array ([*]), then the elements 
        /// are indexed separately. Otherwise, the array is indexed as a whole, 
        /// but only geopoint and aql Analyzers accept array inputs.You cannot 
        /// use an array expansion if <see cref="SearchField"/> is enabled.
        /// Default: false
        /// </summary>
        public bool? SearchField { get; set; }

        /// <summary>
        /// Optional. Enable this option to always cache the field 
        /// normalization values in memory for all fields by default.
        /// This can improve the performance of scoring and ranking queries.
        /// Normalization values are computed for fields which are processed
        /// with Analyzers that have the 
        /// <see cref="AnalyzerApi.Models.AnalyzerFeatures.Norm"/> feature enabled. 
        /// These values are used to score fairer if the same tokens occur 
        /// repeatedly, to emphasize these documents less.
        /// Default: false
        /// </summary>
        public bool? Cache { get; set; }

        /// <summary>
        /// Optional. Can contain an array of paths to additional 
        /// attributes to store in the index. These additional
        /// attributes cannot be used for index lookups or for 
        /// sorting, but they can be used for projections. 
        /// This allows an index to fully cover more queries
        /// and avoid extra document lookups.
        /// </summary>
        public IEnumerable<InvertedIndexStoredValue> StoredValues { get; set; }

        /// <summary>
        /// Optional. You can define a primary sort order to enable an AQL
        /// optimization. If a query iterates over all documents 
        /// of a collection, wants to sort them by attribute values, 
        /// and the (left-most) fields to sort by, as well as their 
        /// sorting direction, match with the primarySort definition,
        /// then the SORT operation is optimized away.
        /// </summary>
        public InvertedIndexSort PrimarySort { get; set; }

        /// <summary>
        /// Optional. Enable this option to always cache the primary key 
        /// column in memory. This can improve the performance of queries 
        /// that return many documents.
        /// Default: false
        /// </summary>
        public bool? PrimaryKeyCache { get; set; }

        /// <summary>
        /// Optional. The name of an Analyzer to use by default. 
        /// This Analyzer is applied to the values of the indexed 
        /// fields for which you don’t define Analyzers explicitly.
        /// Default: identity
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Optional. A list of Analyzer features to use. 
        /// You can set this option to overwrite what features 
        /// are enabled for the default analyzer. Possible values,
        /// <see cref="AnalyzerApi.Models.AnalyzerFeatures"/>
        /// </summary>
        public IEnumerable<string> Features { get; set; }

        /// <summary>
        /// Optional. This option only applies if you use the
        /// inverted index in a search-alias Views.
        /// If set to true, then all document attributes are
        /// indexed, excluding any sub-attributes that are 
        /// configured in the fields array(and their sub-attributes).
        /// The analyzer and features properties apply to the 
        /// sub-attributes.
        /// Default: false
        /// Warning: Using <see cref="IncludeAllFields"/> for a lot
        /// of attributes in combination with complex Analyzers may 
        /// significantly slow down the indexing process.
        /// </summary>
        public bool? IncludeAllFields { get; set; }

        /// <summary>
        ///  Optional. This option only applies if you use the inverted 
        ///  index in a search-alias Views.
        ///  If set to true, then track the value position in arrays for
        ///  array values. For example, when querying a document like 
        ///  { attr: [ "valueX", "valueY", "valueZ" ] }, you need to 
        ///  specify the array element, e.g.doc.attr[1] == "valueY".
        ///  If set to false, all values in an array are treated as equal
        ///  alternatives. You don’t specify an array element in queries,
        ///  e.g.doc.attr == "valueY", and all elements are searched for 
        ///  a match.
        /// </summary>
        public bool? TrackListPositions { get; set; }

        /// <summary>
        /// Optional. The number of threads to use for indexing the fields. 
        /// Default: 2
        /// </summary>
        public int? Parallelism { get; set; }

        /// <summary>
        /// Optional. Wait at least this many commits between removing
        /// unused files in the ArangoSearch data directory 
        /// (default: 2, to disable use: 0). For the case where the 
        /// consolidation policies merge segments often 
        /// (i.e. a lot of commit+consolidate), a lower value will cause 
        /// a lot of disk space to be wasted. For the case where the 
        /// consolidation policies rarely merge segments 
        /// (i.e. few inserts/deletes), a higher value will impact 
        /// performance without any added benefits.
        /// </summary>
        public int? CleanupIntervalStep { get; set; }

        /// <summary>
        /// Optional. Wait at least this many milliseconds between 
        /// committing View data store changes and making documents 
        /// visible to queries (default: 1000, to disable use: 0).
        /// For the case where there are a lot of inserts/updates, 
        /// a lower value, until commit, will cause the index not 
        /// to account for them and memory usage would continue to
        /// grow. For the case where there are a few inserts/updates,
        /// a higher value will impact performance and waste disk
        /// space for each commit call without any added benefits.
        /// </summary>
        public int? CommitIntervalMsec { get; set; }

        /// <summary>
        /// Optional. Wait at least this many milliseconds between
        /// applying ‘consolidationPolicy’ to consolidate View data 
        /// store and possibly release space on the filesystem 
        /// (default: 1000, to disable use: 0). For the case where
        /// there are a lot of data modification operations, a higher 
        /// value could potentially have the data store consume more 
        /// space and file handles. For the case where there are a 
        /// few data modification operations, a lower value will 
        /// impact performance due to no segment candidates available
        /// for consolidation.
        /// </summary>
        public int? ConsolidationIntervalMsec { get; set; }

        /// <summary>
        /// Optional. Maximum number of writers (segments) cached in 
        /// the pool (default: 64, use 0 to disable)
        /// </summary>
        public int? WriteBufferIdle { get; set; }

        /// <summary>
        /// Optional. Maximum number of concurrent active writers 
        /// (segments) that perform a transaction. Other writers 
        /// (segments) wait till current active writers (segments)
        /// finish (default: 0, use 0 to disable)
        /// </summary>
        public int? WriteBufferActive { get; set; }

        /// <summary>
        /// Optional. Maximum memory byte size per writer (segment) 
        /// before a writer (segment) flush is triggered. 0 value 
        /// turns off this limit for any writer (buffer) and data 
        /// will be flushed periodically based on the value defined 
        /// for the flush thread (ArangoDB server startup option). 
        /// 0 value should be used carefully due to high potential 
        /// memory consumption (default: 33554432, use 0 to disable)
        /// </summary>
        public int? WriteBufferSizeMax { get; set; }

        /// <summary>
        /// The consolidation policy to apply for selecting 
        /// which segments should be merged.
        /// </summary>
        public InvertedIndexConsolidationPolicy ConsolidationPolicy { get; set; }
    }
}
