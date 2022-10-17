namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Response from several View API endpoints
    /// </summary>
    public class ViewResponse : ViewDetails, IApiResponseBase
    {
        /// <summary>
        /// The globally unique identifier of the View
        /// </summary>
        public string GloballyUniqueId { get; set; }

        /// <summary>
        /// The id of the view
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Header information from the API
        /// </summary>
        public ApiResponseHeaders ResponseHeaders { get; set; }
    }
}
