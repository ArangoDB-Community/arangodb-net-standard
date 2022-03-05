using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents an AQL query that are finished 
    /// and have exceeded the slow query threshold 
    /// in the selected database.
    /// </summary>
    public class GetSlowAqlQueriesResponseResult
    {
        /// <summary>
        /// The query’s id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the database the query runs in
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The name of the user that started the query
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The query string (potentially truncated)
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The bind parameter values used by the query
        /// </summary>
        public Dictionary<string,object> BindVars { get; set; }

        /// <summary>
        /// The date and time when the query was started
        /// </summary>
        public string Started { get; set; }

        /// <summary>
        /// The query’s total run time
        /// </summary>
        public string RunTime { get; set; }

        /// <summary>
        /// The query’s current execution state 
        /// (will always be “finished” for the list of slow queries)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Indicates whether or not the query uses a streaming cursor
        /// </summary>
        public string Stream { get; set; }
    }

}
