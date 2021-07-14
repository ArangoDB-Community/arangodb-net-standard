using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents a request body for creating an AQL query cursor.
    /// </summary>
    public class PostCursorBody
    {
        /// <summary>
        /// Contains the AQL query to be executed.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The bind parameters of the AQL query.
        /// </summary>
        public Dictionary<string, object> BindVars { get; set; }

        /// <summary>
        /// Extra options for the AQL query.
        /// </summary>
        public PostCursorOptions Options { get; set; }

        /// <summary>
        /// Whether the number of documents in the result set should be returned
        /// in <see cref="CursorResponse{T}.Count"/>.
        /// Calculating the “count” attribute might have a performance impact for some queries
        /// so this option is turned off by default, and “count” is only returned when requested.
        /// </summary>
        public bool? Count { get; set; }

        /// <summary>
        /// Maximum number of result documents to be transferred from the server
        /// to the client in one roundtrip. If this attribute is not set,
        /// a server-controlled default value will be used.
        /// A value of 0 is disallowed.
        /// </summary>
        public long? BatchSize { get; set; }

        /// <summary>
        /// Flag to determine whether the AQL query results cache shall be used.
        /// If set to false, then any query cache lookup will be skipped for the query.
        /// If set to true, it will lead to the query cache being checked for the query
        /// if the query cache mode is either 'on' or 'demand'.
        /// </summary>
        public bool? Cache { get; set; }

        /// <summary>
        /// The maximum number of memory (measured in bytes) that the query is allowed to use.
        /// If set, then the query will fail with error “resource limit exceeded”
        /// in case it allocates too much memory.
        /// A value of 0 indicates that there is no memory limit.
        /// </summary>
        public long? MemoryLimit { get; set; }

        /// <summary>
        /// Gets or set the stream transaction Id.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The time-to-live for the cursor (in seconds).
        /// The cursor will be removed on the server automatically after the specified amount of time.
        /// This is useful to ensure garbage collection of cursors that are not fully fetched by clients.
        /// If not set, a server-defined value will be used (default: 30 seconds).
        /// </summary>
        public int? Ttl { get; set; }
    }

}
