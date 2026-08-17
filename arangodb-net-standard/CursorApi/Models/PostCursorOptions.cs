namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents extra options for the AQL query.
    /// </summary>
    public class PostCursorOptions
    {
        /// <summary>
        /// If set to true and the query contains a LIMIT clause,
        /// then the result will have an extra attribute with the
        /// sub-attributes stats and fullCount,
        /// { ... , "extra": { "stats": { "fullCount": 123 } } }.
        /// The fullCount attribute will contain the number of documents
        /// in the result before the last top-level LIMIT in the query was applied.
        /// It can be used to count the number of documents that match
        /// certain filter criteria, but only return a subset of them, in one go.
        /// It is thus similar to MySQL’s SQL_CALC_FOUND_ROWS hint.
        /// Note that setting the option will disable a few LIMIT optimizations
        /// and may lead to more documents being processed, and thus make queries run longer.
        /// Note that the fullCount attribute may only be present in the result if the query
        /// has a top-level LIMIT clause and the LIMIT clause is actually used in the query.
        /// </summary>
        public bool? FullCount { get; set; }

        /// <summary>
        /// Limits the maximum number of plans that are created by the AQL query optimizer.
        /// </summary>
        public long? MaxPlans { get; set; }

        /// <summary>
        /// Limits the maximum number of warnings a query will return.
        /// The number of warnings a query will return is limited to 10 by default,
        /// but that number can be increased or decreased by setting this attribute.
        /// </summary>
        public long? MaxWarningCount { get; set; }

        /// <summary>
        /// When set to true, the query will throw an exception and abort
        /// instead of producing a warning. This option should be used during development
        /// to catch potential issues early. When the attribute is set to false,
        /// warnings will not be propagated to exceptions and will be returned with the query result.
        /// There is also a server configuration option --query.fail-on-warning for setting
        /// the default value for failOnWarning so it does not need to be set on a per-query level.
        /// </summary>
        public bool? FailOnWarning { get; set; }

        /// <summary>
        /// Specify true and the query will be executed in a streaming fashion. The query result
        /// is not stored on the server, but calculated on the fly. Beware: long-running queries
        /// will need to hold the collection locks for as long as the query cursor exists.
        /// When set to false a query will be executed right away in its entirety. In that case
        /// query results are either returned right away (if the result set is small enough),
        /// or stored on the arangod instance and accessible via the cursor API (with respect to the ttl).
        /// It is advisable to only use this option on short-running queries or without exclusive locks
        /// (write-locks on MMFiles). Please note that the query options cache,
        /// count and fullCount will not work on streaming queries. Additionally query statistics,
        /// warnings and profiling data will only be available after the query is finished.
        /// The default value is false
        /// </summary>
        public bool? Stream { get; set; }

        /// <summary>
        /// A list of to-be-included or to-be-excluded optimizer rules can be put 
        /// into the <see cref="PostCursorOptionsOptimizer.Rules"/> attribute, 
        /// telling the optimizer to include or exclude specific rules.
        /// To disable a rule, prefix its name with a -, to enable a rule, prefix it with a +. 
        /// There is also a pseudo-rule all, which will match all optimizer rules.
        /// </summary>
        public PostCursorOptionsOptimizer Optimizer { get; set; }

        /// <summary>
        /// If set to true or 1, then the additional query profiling information will be returned
        /// in the sub-attribute profile of the extra return attribute, if the query result is not served
        /// from the query cache. Set to 2 the query will include execution stats per query plan node
        /// in sub-attribute stats.nodes of the extra return attribute. Additionally the query plan
        /// is returned in the sub-attribute extra.plan.
        /// </summary>
        public int? Profile { get; set; }

        /// <summary>
        /// This Enterprise Edition parameter allows to configure how long a DBServer will have time
        /// to bring the satellite collections involved in the query into sync. The default value
        /// is 60.0 (seconds). When the max time has been reached the query will be stopped.
        /// </summary>
        public double? SatelliteSyncWait { get; set; }

        /// <summary>
        /// The query has to be executed within the given runtime or it will be killed.
        /// The value is specified in seconds. A value of 0 means no timeout will be enforced.
        /// The default value is 0 (no timeout).
        /// Available in ArangoDB 3.6 onwards.
        /// </summary>
        public double? MaxRuntime { get; set; }

        /// <summary>
        /// Transaction size limit in bytes. Honored by the RocksDB storage engine only.
        /// </summary>
        public long? MaxTransactionSize { get; set; }

        /// <summary>
        /// Maximum total size of operations after which an intermediate commit is performed automatically.
        /// Honored by the RocksDB storage engine only.
        /// </summary>
        public long? IntermediateCommitSize { get; set; }

        /// <summary>
        /// Maximum number of operations after which an intermediate commit is performed automatically.
        /// Honored by the RocksDB storage engine only.
        /// </summary>
        public long? IntermediateCommitCount { get; set; }

        /// <summary>
        /// AQL queries (especially graph traversals) will treat collection to which a user
        /// has no access rights as if these collections were empty. Instead of returning
        /// a forbidden access error, your queries will execute normally.
        /// This is intended to help with certain use-cases: A graph contains several collections
        /// and different users execute AQL queries on that graph. You can now naturally limit
        /// the accessible results by changing the access rights of users on collections.
        /// This feature is only available in the Enterprise Edition.
        /// </summary>
        public bool? SkipInaccessibleCollections { get; set; }

        /// <summary>
        /// Set to true to allow reading from followers in a cluster.
        /// Available in ArangoDB 3.10.0 onwards.
        /// </summary>
        public bool? AllowDirtyReads { get; set; }

        /// <summary>
        /// Set to true to make it possible to retry fetching the latest batch from a cursor.
        /// </summary>
        public bool? AllowRetry { get; set; }

        /// <summary>
        /// Flag to determine whether the AQL query results cache shall be used.
        /// If set to false, then any query cache lookup will be skipped for the query.
        /// If set to true, it will lead to the query cache being checked for the query
        /// if the query cache mode is either 'on' or 'demand'.
        /// </summary>
        public bool? Cache { get; set; }

        /// <summary>
        /// Whether to fill the in-memory block cache with any data read from storage.
        /// If set to false, then any data read is not added to the block cache.
        /// Can be used for queries that read a lot of data that is known to be
        /// not useful in the cache.
        /// </summary>
        public bool? FillBlockCache { get; set; }

        /// <summary>
        /// Maximum number of OR sub-nodes in the internal representation of an AQL query
        /// (DNF - disjunctive normal form) for which an index will be used to evaluate 
        /// a query's filter condition. Above this threshold, the DNF condition will be ignored
        /// and use a full collection scan instead.
        /// Available in ArangoDB 3.11.0 onwards.
        /// </summary>
        public long? MaxDNFConditionMembers { get; set; }

        /// <summary>
        /// Maximum number of calls to a recursive function. If the number of recursive calls
        /// exceeds this threshold, the query is aborted with an error.
        /// </summary>
        public long? MaxNodesPerCallstack { get; set; }

        /// <summary>
        /// Limits the maximum number of query execution plans that are created by the
        /// AQL query optimizer. Use to control the amount of time spent optimizing a query.
        /// </summary>
        public long? MaxNumberOfPlans { get; set; }

        /// <summary>
        /// The threshold for the memory usage (in bytes) after which AQL operators will
        /// start to spill data to disk instead of keeping everything in RAM. Only certain
        /// operations like SORT or COLLECT support spilling to disk.
        /// Available in ArangoDB 3.10.0 onwards.
        /// </summary>
        public long? SpillOverThresholdMemoryUsage { get; set; }

        /// <summary>
        /// The threshold for the number of rows after which AQL operators will start to
        /// spill data to disk instead of keeping everything in RAM. Only certain
        /// operations like SORT or COLLECT support spilling to disk.
        /// Available in ArangoDB 3.10.0 onwards.
        /// </summary>
        public long? SpillOverThresholdNumRows { get; set; }

        /// <summary>
        /// Whether to use the query plan cache. If set to true, the query plan cache will
        /// be checked for an existing plan before optimizing the query. If set to false,
        /// the query will be optimized without checking the plan cache.
        /// Available in ArangoDB 3.12.4 onwards.
        /// </summary>
        public bool? UsePlanCache { get; set; }
    }
}
