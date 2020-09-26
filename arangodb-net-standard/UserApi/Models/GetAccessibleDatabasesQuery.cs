namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents query parameters used when fetching the list of databases
    /// available to a user.
    /// </summary>
    public class GetAccessibleDatabasesQuery
    {
        /// <summary>
        /// Whether to return the full set of access levels
        /// for all databases and all collections.
        /// </summary>
        public bool? Full { get; set; }

        internal string ToQueryString()
        {
            if (Full != null)
            {
                return "full=" + Full.ToString().ToLower();
            }
            else
            {
                return "";
            }
        }
    }
}
