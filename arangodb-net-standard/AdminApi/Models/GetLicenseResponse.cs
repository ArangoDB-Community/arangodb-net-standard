namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerRoleAsync"/>
    /// </summary>
    public class GetLicenseResponse
    {
        /// <summary>
        /// Features of the current license
        /// </summary>
        public LicenseFeatures Features { get; set; }

        /// <summary>
        /// Encrypted and base64-encoded license key
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// License version number
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The status of the license.
        /// Possible values for role are:
        /// good: The license is valid for more than 2 weeks.
        /// expiring: The license is valid for less than 2 weeks.
        /// expired: The license has expired. In this situation, no new Enterprise Edition features can be utilized.
        /// read-only: The license is expired over 2 weeks. The instance is now restricted to read-only mode.
        /// </summary>
        public string Status { get; set; }
    }
}