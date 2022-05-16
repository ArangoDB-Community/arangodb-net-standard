namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Defines values for ArangoDB index types.
    /// </summary>
    public static class IndexTypes
    {
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-fulltext.html
        /// </summary>
        public const string FullText = "fulltext";

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-geo.html
        /// </summary>
        public const string Geo = "geo";

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-persistent.html
        /// </summary>
        public const string Persistent = "persistent";

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-ttl.html
        /// </summary>
        public const string TTL = "ttl";
    }
}
