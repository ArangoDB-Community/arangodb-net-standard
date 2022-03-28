namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class ViewConsolidationPolicy
    {
        public string Type  { get; set; }
        public decimal? Threshold { get; set; }
        public int? SegmentsBytesFloor { get; set; }
        public int? SegmentsBytesMax { get; set; }
        public int? SegmentsMax { get; set; }
        public int? SegmentsMin { get; set; }
        public int? MinScore { get; set; }
    }

}
