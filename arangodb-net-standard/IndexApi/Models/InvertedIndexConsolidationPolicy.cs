using System;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// The consolidation policy to apply for selecting 
    /// which segments should be merged.
    /// </summary>
    public class InvertedIndexConsolidationPolicy
    {
        /// <summary>
        /// The segment candidates for the “consolidation”
        /// operation are selected based upon several possible
        /// configurable formulas as defined by their types. 
        /// Only value allowed: "tier" (default): consolidate 
        /// based on segment byte size and live document count 
        /// as dictated by the customization attributes.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Optional. Defines the value (in bytes) to treat all
        /// smaller segments as equal for consolidation selection. 
        /// Default: 2097152
        /// </summary>
        public int? SegmentsBytesFloor { get; set; }

        /// <summary>
        /// Optional. The maximum allowed size of all consolidated 
        /// segments in bytes.
        /// Default: 5368709120
        /// </summary>
        public long? SegmentsBytesMax { get; set; }

        /// <summary>
        /// Optional. The maximum number of segments that are evaluated
        /// as candidates for consolidation. 
        /// Default: 10
        /// </summary>
        public int? SegmentsMax { get; set; }

        /// <summary>
        /// Optional. The minimum number of segments that are evaluated 
        /// as candidates for consolidation. 
        /// Default: 1
        /// </summary>
        public int? SegmentsMin { get; set; }

        /// <summary>
        /// Optional. Filter out consolidation candidates with a score 
        /// less than this. 
        /// Default: 0
        /// </summary>
        public int? MinScore { get; set; }
    }
}
