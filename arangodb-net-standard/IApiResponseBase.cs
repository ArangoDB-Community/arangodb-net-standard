namespace ArangoDBNetStandard
{
    /// <summary>
    /// Base class for all API response
    /// </summary>
    public interface IApiResponseBase
    {
        /// <summary>
        /// Header information from the API
        /// </summary>
        ApiResponseHeaders ResponseHeaders { get; set; }
    }
}
