namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// The status of the in memory graph.
    /// </summary>
    public class PregelJobGraphStoreStatus
    {
        /// <summary>
        /// The number of vertices that are loaded 
        /// from the database into memory.
        /// </summary>
        public int? VerticesLoaded { get; set; }

        /// <summary>
        ///  The number of edges that are loaded 
        ///  from the database into memory.
        /// </summary>
        public int? EdgesLoaded { get; set; }

        /// <summary>
        /// The number of bytes used in-memory
        /// for the loaded graph.
        /// </summary>
        public long? MemoryBytesUsed { get; set; }

        /// <summary>
        /// The number of vertices that are 
        /// written back to the database after
        /// the Pregel computation finished. 
        /// It is only set if the store parameter
        /// is set to true.
        /// </summary>
        public int? VerticesStored { get; set; }
    }
}