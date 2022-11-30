using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Parameters for <see cref="IAdminApiClient.GetLogsAsync"/>
    /// </summary>
    public class GetLogsQuery
    {
        public const string SortAscending = "asc";
        public const string SortDescending = "desc";

        /// <summary>
        /// Optional. Retrieves all log entries 
        /// up to this log level.
        /// </summary>
        public LogLevel? UpTo { get; set; }

        /// <summary>
        /// Optional. Retrieves all log entries 
        /// of this log level. Note that the query
        /// parameters <see cref="UpTo"/> and <see cref="Level"/> 
        /// are mutually exclusive.
        /// </summary>
        public LogLevel? Level { get; set; }

        /// <summary>
        /// Optional. Retrieves all log entries
        /// that their log entry identifier (lid value) 
        /// is greater or equal to this value.
        /// </summary>
        public long? Start { get; set; }
        
        /// <summary>
        /// Optional. Specifies the number of 
        /// log entries to retrieve.
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// Optional. Specifies the number of 
        /// records to skip when retrieving 
        /// log entries. 
        /// <see cref="Offset"/> and <see cref="Size"/>
        /// can be used for pagination.
        /// </summary>
        public long? Offset { get; set; }

        /// <summary>
        /// Optional. Retrieves all log entries 
        /// containing this text string.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Optional. Specify whether to sort 
        /// log entries in ascending or descending order.
        /// Only <see cref="SortAscending"/> or <see cref="SortDescending"/>
        /// are accepted here.
        /// Log entries are sorted by their Id values.
        /// The default value is <see cref="SortAscending"/>.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Optional. Retrieves all log entries of 
        /// the specified server. All other query 
        /// parameters remain valid. If no serverId 
        /// is given, the asked server will reply. 
        /// This parameter is only meaningful on Coordinators.
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// Generates the querystring
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            var queryParams = new List<string>();
            if (UpTo != null)
            {
                queryParams.Add("upto=" + ((int)UpTo).ToString());
            }
            if (Level != null)
            {
                queryParams.Add("level=" + Level.ToString());
            }
            if (Start != null)
            {
                queryParams.Add("start=" + Start.ToString());
            }
            if (Size != null)
            {
                queryParams.Add("size=" + Size.ToString());
            }
            if (Offset != null)
            {
                queryParams.Add("offset=" + Offset.ToString());
            }
            if (!string.IsNullOrEmpty(Search))
            {
                queryParams.Add("search=" + Search);
            }
            if (!string.IsNullOrEmpty(Sort))
            {
                queryParams.Add("sort=" + Sort);
            }
            if (!string.IsNullOrEmpty(ServerId))
            {
                queryParams.Add("serverId=" + ServerId);
            }
            return string.Join("&", queryParams);
        }
    }
}