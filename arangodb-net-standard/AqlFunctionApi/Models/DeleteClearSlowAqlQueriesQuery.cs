namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Generates query string for 
    /// <see cref="AqlFunctionApiClient.DeleteClearSlowAqlQueriesAsync(DeleteClearSlowAqlQueriesQuery)"/>
    /// </summary>
    public class DeleteClearSlowAqlQueriesQuery
    {
        /// <summary>
        /// If set to true, it will clear the slow query history 
        /// in all databases, not just the selected one. 
        /// Using the parameter is only allowed in the system 
        /// database and with superuser privileges.
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