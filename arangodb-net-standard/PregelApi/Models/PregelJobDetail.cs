using System.Collections.Generic;

namespace ArangoDBNetStandard.PregelApi.Models
{
    public class PregelJobDetail
    {
        /// <summary>
        /// The aggregated details of the full Pregel run. 
        /// The values are totals of all the DB-Server.
        /// </summary>
        public PregelJobDetailInfo AggregatedStatus { get; set; }

        /// <summary>
        /// The details of the Pregel for every DB-Server.
        /// Each object key is a DB-Server ID, and each 
        /// value is a nested object similar to the 
        /// <see cref="AggregatedStatus"/> attribute. 
        /// </summary>
        public Dictionary<string, PregelJobDetailInfo> WorkerStatus { get; set; }
    }
}