namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutCollectionPropertyBody
    {
        public bool? WaitForSync { get; set; }

        [System.Obsolete()]
        public long? JournalSize { get; set; }
    }
}
