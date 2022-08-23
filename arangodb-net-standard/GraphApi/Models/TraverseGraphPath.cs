using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class TraverseGraphPath
    {
        /// <summary>
        /// An array of all vertices on this path
        /// </summary>
        public IEnumerable<string> Vertices { get; set; }

        /// <summary>
        /// An array of all edges on this path
        /// </summary>
        public IEnumerable<string> Edges { get; set; }
    }
}