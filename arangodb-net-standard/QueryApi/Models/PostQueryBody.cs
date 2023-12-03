using System.Collections.Generic;

namespace ArangoDBNetStandard.QueryApi.Models
{
    public class PostQueryBody
    {
        /// <summary>
        /// Contains the AQL query to be executed.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The bind parameters of the AQL query.
        /// </summary>
        public Dictionary<string, object> BindVars { get; set; }
    }
}
