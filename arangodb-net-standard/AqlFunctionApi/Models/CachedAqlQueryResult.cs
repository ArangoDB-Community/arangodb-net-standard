using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from <see cref="AqlFunctionApiClient.GetCachedAqlQueryResultsAsync()"/>
    /// Represents a cached AQL query result.
    /// </summary>
    public class CachedAqlQueryResult
    {
        /// <summary>
        /// The query result’s hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The query string
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The query’s bind parameters. This attribute is only
        /// shown if tracking for bind variables was enabled 
        /// at server start
        /// </summary>
        public Dictionary<string, object> BindVars { get; set; }

        /// <summary>
        /// The size of the query result and bind parameters, in bytes
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// The number of documents/rows in the query result
        /// </summary>
        public int? Results { get; set; }

        /// <summary>
        /// The date and time when the query was stored in the cache
        /// </summary>
        public DateTime? Started { get; set; }

        /// <summary>
        /// The number of times the result was served from the cache
        /// (can be 0 for queries that were only stored in the cache 
        /// but were never accessed again afterwards)
        /// </summary>
        public int? Hits { get; set; }

        /// <summary>
        /// The query’s run time
        /// </summary>
        public TimeSpan RunTime { get; set; }

        /// <summary>
        /// An array of collections/Views the query was using
        /// </summary>
        public IList<string> DataSources { get; set; }
    }
}