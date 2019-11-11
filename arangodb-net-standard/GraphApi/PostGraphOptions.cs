namespace ArangoDBNetStandard.GraphApi
{
    public class PostGraphOptions
    {
        public string SmartGraphAttribute { get; set; }

        public int NumberOfShards { get; set; }

        public int ReplicationFactor { get; set; }
    }
}
