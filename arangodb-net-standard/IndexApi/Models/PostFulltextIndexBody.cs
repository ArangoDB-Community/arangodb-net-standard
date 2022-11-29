namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a Fulltext index
    /// </summary>
    public class PostFulltextIndexBody : PostIndexBody
    {
        public PostFulltextIndexBody()
        {
            Type = IndexTypes.Fulltext;
        }

        /// <summary>
        /// Required. Minimum character length of words to
        /// index. Will default to a server-defined value 
        /// if unspecified. It is thus recommended to set
        /// this value explicitly when creating the index.
        /// </summary>
        public long MinLength { get; set; }
    }
}