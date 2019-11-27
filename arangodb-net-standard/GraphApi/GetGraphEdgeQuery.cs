﻿using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents query parameters used when fetching an edge in a graph.
    /// </summary>
    public class GetGraphEdgeQuery
    {
        /// <summary>
        /// Must contain a revision.
        /// If this is set, a document is only returned if it has exactly this revision.
        /// </summary>
        public string Rev { get; set; }

        public string ToQueryString()
        {
            if (Rev != null)
            {
                return "rev=" + Rev;
            }
            else
            {
                return "";
            }
        }
    }
}