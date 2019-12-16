namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutCollectionPropertyBody
    {
        public bool? WaitForSync { get; set; }

        public long? JournalSize { get; set; }
    }
}
