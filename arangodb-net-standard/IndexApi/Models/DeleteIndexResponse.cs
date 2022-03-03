namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents a response returned after deleting an index.
    /// </summary>
    public class DeleteIndexResponse : ResponseBase
    {
        /// <summary>
        /// Id of the index
        /// </summary>
        public string Id { get; set; }
    }
}
