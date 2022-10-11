using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Defines values for ArangoDB view types.
    /// Possible value for <see cref="ViewDetails.Type"/>
    /// </summary>
    public static class ViewTypes
    {
        /// <summary>
        /// See https://www.arangodb.com/docs/stable/arangosearch-views.html
        /// </summary>
        public const string ArangoSearch = "arangosearch";

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/arangosearch-views-search-alias.html
        /// </summary>
        public const string SearchAlias = "search-alias";
    }
}