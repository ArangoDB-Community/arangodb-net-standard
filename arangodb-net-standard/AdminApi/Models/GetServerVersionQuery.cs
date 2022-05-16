using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Parameters for <see cref="IAdminApiClient.GetServerVersionAsync(GetServerVersionQuery)"/>
    /// </summary>
    public class GetServerVersionQuery
    {
        /// <summary>
        /// If set to true, the response will 
        /// contain a details attribute with 
        /// additional information about included 
        /// components and their versions.
        /// The attribute names and internals of
        /// the details object may vary depending 
        /// on platform and ArangoDB version.
        /// </summary>
        public bool? Details { get; set; }

        /// <summary>
        /// Generates the querystring
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            var queryParams = new List<string>();
            if (Details != null)
            {
                queryParams.Add("details=" + Details.ToString());
            }
            return string.Join("&", queryParams);
        }
    }
}