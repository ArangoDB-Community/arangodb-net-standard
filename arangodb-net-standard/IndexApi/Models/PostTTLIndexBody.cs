namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a TTL (time-to-live) index
    /// </summary>
    public class PostTTLIndexBody : PostIndexBody
    {
        public PostTTLIndexBody()
        {
            Type = IndexTypes.TTL;
        }

        /// <summary>
        /// Required. The time interval (in seconds) from the point 
        /// in time stored in the fields attribute after
        /// which the documents count as expired. Can be 
        /// set to 0 to let documents expire as soon as 
        /// the server time passes the point in time stored
        /// in the document attribute, or to a higher 
        /// number to delay the expiration.
        /// </summary>
        public int ExpireAfter { get; set; }
    }
}