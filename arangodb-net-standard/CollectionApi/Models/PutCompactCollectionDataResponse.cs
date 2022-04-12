namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutCompactCollectionDataResponse : ResponseBase
    {
        public int? Type { get; set; }
        public int? Status { get; set; }
        public bool? IsSystem { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string GloballyUniqueId { get; set; }
    }
}