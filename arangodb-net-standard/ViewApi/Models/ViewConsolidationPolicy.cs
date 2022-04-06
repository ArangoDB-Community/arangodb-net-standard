namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// The consolidation policy to apply
    /// for selecting which segments should be merged.
    /// Read more about this in the documentation.
    /// </summary>
    public class ViewConsolidationPolicy
    {
        /// <summary>
        /// The type of consolidation policy.
        /// 1) "tier" (default): consolidate 
        /// based on segment byte size and live
        /// document count as dictated by the 
        /// customization attributes. 
        /// If this type is used, then below 
        /// <see cref="SegmentsBytesFloor"/>, 
        /// <see cref="SegmentsBytesMax"/>,
        /// <see cref="SegmentsMax"/>, 
        /// <see cref="SegmentsMin"/> and 
        /// <see cref="MinScore"/> are specified.
        /// 2) "bytes_accum": consolidate only
        /// if the sum of all candidate segment
        /// byte size is less than the total 
        /// segment byte size multiplied by 
        /// the <see cref="ViewConsolidationPolicy.Threshold"/>.
        /// </summary>
        public string Type  { get; set; }

        /// <summary>
        /// Value in the range [0.0, 1.0]
        /// </summary>
        public decimal? Threshold { get; set; }

        /// <summary>
        /// The value (in bytes) to treat all 
        /// smaller segments as equal for
        /// consolidation selection (default: 2097152)
        /// </summary>
        public int? SegmentsBytesFloor { get; set; }

        /// <summary>
        /// Maximum allowed size of all consolidated
        /// segments in bytes (default: 5368709120)
        /// </summary>
        public int? SegmentsBytesMax { get; set; }

        /// <summary>
        /// The maximum number of segments that
        /// will be evaluated as candidates 
        /// for consolidation (default: 10)
        /// </summary>
        public int? SegmentsMax { get; set; }

        /// <summary>
        /// The minimum number of segments that 
        /// will be evaluated as candidates
        /// for consolidation (default: 1)
        /// </summary>
        public int? SegmentsMin { get; set; }

        /// <summary>
        /// Specified if <see cref="Type"/> is "tier".
        /// </summary>
        public int? MinScore { get; set; }
    }
}