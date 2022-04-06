namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents the global properties of the AQL Query Cache
    /// Returned by
    /// <see cref="AqlFunctionApiClient.GetQueryTrackingConfigurationAsync()"/>
    /// and 
    /// <see cref="AqlFunctionApiClient.PutChangeQueryTrackingConfigurationAsync(PutChangeQueryTrackingConfigurationBody)"/>
    /// </summary>
    public class QueryTrackingConfiguration : ResponseBase
    {
        /// <summary>
        /// If set to true, then queries will be tracked. 
        /// If set to false, neither queries nor slow 
        /// queries will be tracked.
        /// </summary>
        public string Enabled { get; set; }

        /// <summary>
        /// If set to true, then slow queries will be 
        /// tracked in the list of slow queries if their
        /// runtime exceeds the value set in slowQueryThreshold. 
        /// In order for slow queries to be tracked,
        /// the enabled property must also be set to true.
        /// </summary>
        public bool? TrackSlowQueries { get; set; }

        /// <summary>
        ///  If set to true, then the bind variables used
        ///  in queries will be tracked along with queries.
        /// </summary>
        public bool? TrackBindVars { get; set; }

        /// <summary>
        /// The maximum number of slow queries to keep in 
        /// the list of slow queries. If the list of slow 
        /// queries is full, the oldest entry in it will 
        /// be discarded when additional slow queries occur.
        /// </summary>
        public int? MaxSlowQueries { get; set; }

        /// <summary>
        /// The threshold value for treating a query as slow.
        /// A query with a runtime greater or equal to this 
        /// threshold value will be put into the list of
        /// slow queries when slow query tracking is enabled. 
        /// The value for slowQueryThreshold is specified in seconds.
        /// </summary>
        public int? SlowQueryThreshold { get; set; }

        /// <summary>
        /// See online documentation
        /// </summary>
        public int? SlowStreamingQueryThreshold { get; set; }

        /// <summary>
        /// The maximum query string length to keep in the list 
        /// of queries. Query strings can have arbitrary lengths, 
        /// and this property can be used to save memory in case 
        /// very long query strings are used. 
        /// The value is specified in bytes.
        /// </summary>
        public int? MaxQueryStringLength { get; set; }
    }
}