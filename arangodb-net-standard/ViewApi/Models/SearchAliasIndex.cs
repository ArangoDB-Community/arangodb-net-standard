using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.ViewApi.Models
{
    public class SearchAliasIndex
    {
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>
        /// Name of the index
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Optional. Type of operation.
        /// Possible values:
        /// <see cref="SearchAliasIndexOperation.Add"/>, or
        /// <see cref="SearchAliasIndexOperation.Del"/>
        /// Only relevant when updating the 
        /// properties of a View definition.
        /// </summary>
        public string Operation { get; set; }
    }
}