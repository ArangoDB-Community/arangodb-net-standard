using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Parameters for <see cref="IAdminApiClient.GetLogsAsync(GetLogsQuery)"/>
    /// </summary>
    public class PutLicenseQuery
    {
        /// <summary>
        /// If the new license expires sooner 
        /// than the current one force an update,
        /// do not return an error. 
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Generates the querystring
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            var queryParams = new List<string>();
            queryParams.Add("force=" + Force.ToString());
            return string.Join("&", queryParams);
        }
    }
}