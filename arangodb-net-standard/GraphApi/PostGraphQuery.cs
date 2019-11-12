namespace ArangoDBNetStandard.GraphApi
{
    public class PostGraphQuery
    {
        public string SmartGraphAttribute { get; set; }

        public int NumberOfShards { get; set; }

        public int ReplicationFactor { get; set; }
    }
}
