using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Type of index
    /// </summary>
    public enum IndexType
    {
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-fulltext.html
        /// </summary>
        FullText,
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-geo.html
        /// </summary>
        Geo,
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-persistent.html
        /// </summary>
        Persistent,
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/indexes-ttl.html
        /// </summary>
        TTL
    }
}
