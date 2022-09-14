namespace ArangoDBNetStandard.PregelApi.Models
{
    public class PregelJobAggregatedStatus
    {
        public string TimeStamp { get; set; }
        public PregelJobGraphStoreStatus GraphStoreStatus { get; set; }
        public PregelJobGssStatus AllGssStatus { get; set; }
        public object WorkerStatus { get; set; }
    }
}