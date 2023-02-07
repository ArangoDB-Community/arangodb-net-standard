using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from
    /// <see cref="AqlFunctionApiClient.GetCurrentlyRunningAqlQueriesAsync"/>
    /// </summary>
    public class RunningAqlQuery
    {
        /// <summary>
        /// The query’s id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the database the query runs in.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The name of the user that started the query.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The query string (potentially truncated).
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The bind parameter values used by the query.
        /// </summary>
        public Dictionary<string, object> BindVars { get; set; }

        /// <summary>
        /// The date and time when the query was started. 
        /// </summary>
        public DateTime? Started { get; set; }

        /// <summary>
        /// The query’s run time up to the point the list of queries was queried.
        /// </summary>
        public string RunTime { get; set; }

        /// <summary>
        /// The query’s current execution state.
        /// See online documentation 
        /// for more information.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Indicates whether or not the query uses a streaming cursor.
        /// </summary>
        public bool? Stream { get; set; }
    }
}