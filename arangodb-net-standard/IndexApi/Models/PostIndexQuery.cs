namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents query parameters used when creating a new index
    /// for a collection.
    /// </summary>
    public class PostIndexQuery
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
