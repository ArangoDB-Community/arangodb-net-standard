using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi
{
    public class CursorResponseStats
    {
        /// <summary>
        /// When <see cref="PostCursorOptions.FullCount"/> is used,
        /// the fullCount attribute will contain the number of documents in the result
        /// before the last top-level LIMIT in the query was applied.
        /// </summary>
        public long? FullCount { get; set; }

        /// <summary>
        /// Populated when <see cref="PostCursorOptions.Profile"/> is set to 2.
        /// </summary>
        public IEnumerable<CursorResponseStatsNode> Nodes { get; set; }

        public long WritesExecuted { get; set; }

        public long WritesIgnored { get; set; }

        public double ExecutionTime { get; set; }

        public long PeakMemoryUsage { get; set; }

        public long ScannedFull { get; set; }

        public long ScannedIndex { get; set; }

        public long Filtered { get; set; }

        public long HttpRequests { get; set; }
    }
}