using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerVersionAsync"/>
    /// </summary>
    public class GetServerVersionResponse
    {
        /// <summary>
        /// The type of server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The type of license.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// The version of the server.
        /// </summary>
        public string Version { get; set; }              

        /// <summary>
        /// Additional details about the DB server. 
        /// This is returned only if the <see cref="GetServerVersionQuery.Details"/> 
        /// query parameter is set to true in 
        /// the request.
        /// </summary>
        public Dictionary<string, string> Details {get;set;}
    }
}