using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Possible operations for search alias indexes.
    /// </summary>
    public static class SearchAliasIndexOperation
    {
        /// <summary>
        /// Add the index to the stored indexes 
        /// property of the View.
        /// </summary>
        public const string Add = "add";

        /// <summary>
        /// Remove the index from the stored indexes 
        /// property of the View.
        /// </summary>
        public const string Del = "del";
    }
}