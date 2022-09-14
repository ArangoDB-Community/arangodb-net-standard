namespace ArangoDBNetStandard.PregelApi.Models
{
    public class PregelJobGraphStoreStatus
    {
        public int? VerticesLoaded { get; set; }
        public int? EdgesLoaded { get; set; }
        public long? MemoryBytesUsed { get; set; }
        public int? VerticesStored { get; set; }
    }
}