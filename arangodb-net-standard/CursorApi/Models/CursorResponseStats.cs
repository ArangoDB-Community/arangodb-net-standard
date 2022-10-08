using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi.Models
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

        #region Introduced in v3.10

        /// <summary>
        /// Introduced in v3.10.
        /// The total number of cursor objects created during 
        /// query execution. Cursor objects are created for 
        /// index lookups.
        /// </summary>
        public long? CursorsCreated { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// The total number of times an existing cursor 
        /// object was repurposed. 
        /// </summary>
        /// <remarks>
        /// Repurposing an existing 
        /// cursor object is normally more efficient compared 
        /// to destroying an existing cursor object and 
        /// creating a new one from scratch.
        /// </remarks>
        public long? CursorsRearmed { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// The total number of index entries read from in-memory 
        /// caches for indexes of type edge or persistent. 
        /// This value will only be non-zero when reading from 
        /// indexes that have an in-memory cache enabled, and 
        /// when the query allows using the in-memory cache 
        /// (i.e. using equality lookups on all index attributes).
        /// </summary>
        public long? CacheHits { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// The total number of cache read attempts for index entries
        /// that could not be served from in-memory caches for indexes
        /// of type edge or persistent. This value will only be non-zero
        /// when reading from indexes that have an in-memory cache 
        /// enabled, the query allows using the in-memory cache 
        /// (i.e. using equality lookups on all index attributes) and
        /// the looked up values are not present in the cache.
        /// </summary>
        public long? CacheMisses { get; set; }

        #endregion
    }
}