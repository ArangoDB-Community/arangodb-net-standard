namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// The details of a Pregel run.
    /// </summary>
    public class PregelJobDetailInfo
    {
        /// <summary>
        /// The time at which the status was measured.
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// The status of the in memory graph.
        /// </summary>
        public PregelJobGraphStoreStatus GraphStoreStatus { get; set; }

        /// <summary>
        /// Information about the global supersteps.
        /// </summary>
        public PregelJobGssStatus AllGssStatus { get; set; }
    }
}