namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Defines features of a license
    /// </summary>
    public class LicenseFeatures 
    {
        /// <summary>
        /// License expiry date.
        /// </summary>
        /// <remarks>
        /// Unix timestamp (seconds since January 1st, 1970 UTC)
        /// </remarks>
        public long Expires { get; set; }
    }
}