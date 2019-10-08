namespace ArangoDBNetStandard.CollectionApi
{
    public class CollectionKeyOptions
    {
        public bool AllowUserKeys { get; set; }

        public long Increment { get; set; }

        public long Offset { get; set; }

        public string Type { get; set; }
    }
}