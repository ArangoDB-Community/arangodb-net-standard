namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents a request body to create a new graph vertex collection.
    /// </summary>
    public class PostVertexCollectionBody
    {
        /// <summary>
        /// The name of the vertex collection to create.
        /// </summary>
        public string Collection { get; set; }
    }
}
