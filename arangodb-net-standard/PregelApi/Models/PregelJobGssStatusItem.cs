namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// Details for each global superstep.
    /// </summary>
    public class PregelJobGssStatusItem
    {
        /// <summary>
        ///  The number of vertices that have been 
        ///  processed in this step.
        /// </summary>
        public int? VerticesProcessed { get; set; }

        /// <summary>
        /// The number of messages sent in this step.
        /// </summary>
        public int? MessagesSent { get; set; }

        /// <summary>
        /// The number of messages received in this step.
        /// </summary>
        public int? MessagesReceived { get; set; }

        /// <summary>
        /// The number of bytes used in memory for the 
        /// messages in this step.
        /// </summary>
        public long? MemoryBytesUsedForMessages { get; set; }
    }
}