namespace ArangoDBNetStandard.PregelApi.Models
{
    public class PregelJobGssStatusItem
    {
        public int? VerticesProcessed { get; set; }
        public int? MessagesSent { get; set; }
        public int? MessagesReceived { get; set; }
        public long? MemoryBytesUsedForMessages { get; set; }
    }
}