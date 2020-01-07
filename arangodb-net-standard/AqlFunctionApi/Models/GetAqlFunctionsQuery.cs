using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents query parameters used when getting all AQL user functions.
    /// </summary>
    public class GetAqlFunctionsQuery
    {
        /// <summary>
        /// Returns all registered AQL user functions from this namespace under result.
        /// </summary>
        public string Namespace { get; set; }

        internal string ToQueryString()
        {
            if (Namespace != null)
            {
                return "namespace=" + Namespace;
            }
            else
            {
                return "";
            }
        }
    }
}
