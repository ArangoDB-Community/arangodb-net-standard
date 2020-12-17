namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionsResponseResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public CollectionType Type { get; set; }

        public bool IsSystem { get; set; }

        public string GloballyUniqueId { get; set; }
    }
}
