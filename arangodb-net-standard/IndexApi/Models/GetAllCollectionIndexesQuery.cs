namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents query parameters used when fetching the list of indexes 
    /// for a collection.
    /// </summary>
    public class GetAllCollectionIndexesQuery
    {
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string CollectionName { get; set; }

        internal string ToQueryString()
        {
            if (!string.IsNullOrEmpty(CollectionName))
            {
                return "collection=" + CollectionName.ToString().ToLower();
            }
            else
            {
                return "";
            }
        }
    }
}
