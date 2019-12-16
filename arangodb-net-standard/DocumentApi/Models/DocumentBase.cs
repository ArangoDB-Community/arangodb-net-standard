namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Base model for POST document responses.
    /// </summary>
    public class DocumentBase
    {
        /// <summary>
        /// ArangoDB document key.
        /// </summary>
        public string _key { get; set; }

        /// <summary>
        /// ArangoDB document ID.
        /// </summary>
        public string _id { get; set; }

        /// <summary>
        /// ArangoDB document revision tag.
        /// </summary>
        public string _rev { get; set; }
    }
}
