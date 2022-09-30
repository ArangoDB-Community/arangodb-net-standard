using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.ViewApi.Models
{
    public class SearchAliasIndex
    {
        /// <summary>
        /// Possible value for <see cref="Operation"/>
        /// </summary>
        public const string SearchAliasIndexAddOperation = "add";

        /// <summary>
        /// Possible value for <see cref="Operation"/>
        /// </summary>
        public const string SearchAliasIndexDelOperation = "del";

        /// <summary>
        /// Possible value for <see cref="Operation"/>
        /// </summary>
        public const string SearchAliasIndexNoOperation = "";

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
        /// <see cref="SearchAliasIndexAddOperation"/>,
        /// <see cref="SearchAliasIndexDelOperation"/>, or
        /// <see cref="SearchAliasIndexNoOperation"/>
        /// </summary>
        public string Operation { get; set; }
    }
}