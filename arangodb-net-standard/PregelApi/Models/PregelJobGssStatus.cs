using System.Collections.Generic;

namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// Information about the global supersteps.
    /// </summary>
    public class PregelJobGssStatus
    {
        /// <summary>
        /// A list of objects with details for each global superstep.
        /// </summary>
        public List<PregelJobGssStatusItem> Items { get; set; }
    }
}