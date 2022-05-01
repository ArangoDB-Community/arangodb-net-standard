using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Response from <see cref="IAdminApiClient.GetLogsAsync(GetLogsQuery)"/>
    /// </summary>
    public class GetLogsResponse
    {
        public int Total { get; set; }
        public List<LogMessage> Messages { get; set; }
    }
}