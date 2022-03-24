namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Generates query string for 
    /// <see cref="AqlFunctionApiClient.GetCurrentlyRunningAqlQueriesAsync(GetCurrentlyRunningAqlQueriesQuery)"/>
    /// </summary>
    public class GetCurrentlyRunningAqlQueriesQuery
    {
        /// <summary>
        /// If set to true, will return the currently
        /// running queries in all databases, not just
        /// the selected one. Using the parameter is 
        /// only allowed in the system database and
        /// with superuser privileges.
        /// </summary>
        public bool? All { get; set; }

        internal string ToQueryString()
        {
            if (All != null)
            {
                return "all=" + All.ToString().ToLower();
            }
            else
            {
                return "";
            }
        }
    }   
}