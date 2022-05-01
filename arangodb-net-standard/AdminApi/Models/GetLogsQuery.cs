using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Parameters for <see cref="IAdminApiClient.GetLogsAsync(GetLogsQuery)"/>
    /// </summary>
    public class GetLogsQuery
    {
        public LogLevel? UpTo { get; set; }
        public LogLevel? Level { get; set; }
        public long? Start { get; set; }
        public long? Size { get; set; }
        public long? Offset { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public string ServerId { get; set; }
        internal string ToQueryString()
        {
            var queryParams = new List<string>();
            if (UpTo != null)
            {
                queryParams.Add("upto=" + ((int)UpTo).ToString());
            }
            if (Level != null)
            {
                queryParams.Add("level=" + UpTo.ToString());
            }
            if (Start != null)
            {
                queryParams.Add("start=" + Start.ToString());
            }
            if (Size != null)
            {
                queryParams.Add("size=" + Size.ToString());
            }
            if (Offset != null)
            {
                queryParams.Add("offset=" + Offset.ToString());
            }
            if (!string.IsNullOrEmpty(Search))
            {
                queryParams.Add("search=" + Search);
            }
            if (!string.IsNullOrEmpty(Sort))
            {
                queryParams.Add("sort=" + Sort);
            }
            if (!string.IsNullOrEmpty(ServerId))
            {
                queryParams.Add("serverId=" + ServerId);
            }
            return string.Join("&", queryParams);
        }
    }
}