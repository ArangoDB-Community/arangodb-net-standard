namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents the global properties of the AQL Query Cache
    /// Response from
    /// <see cref="AqlFunctionApiClient.GetQueryCacheGlobalPropertiesAsync()"/>
    /// </summary>
    public class QueryCacheGlobalProperties
    {
        /// <summary>
        /// The mode the AQL query results cache operates in. 
        /// The mode is one of the following values: off,
        /// on or demand.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// The maximum number of query results that will
        /// be stored per database-specific cache.
        /// </summary>
        public int? MaxResults { get; set; }

        /// <summary>
        /// The maximum cumulated size of query results
        /// that will be stored per database-specific cache.
        /// </summary>
        public int? MaxResultsSize { get; set; }

        /// <summary>
        /// The maximum individual result size of queries 
        /// that will be stored per database-specific cache.
        /// </summary>
        public int? MaxEntrySize { get; set; }

        /// <summary>
        /// Whether or not results of queries that involve
        /// system collections will be stored in the query
        /// results cache.
        /// </summary>
        public bool? IncludeSystem { get; set; }
    }
}