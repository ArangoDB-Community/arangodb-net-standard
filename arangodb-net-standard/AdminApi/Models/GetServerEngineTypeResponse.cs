namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerEngineTypeAsync"/>
    /// </summary>
    public class GetServerEngineTypeResponse
    {
        /// <summary>
        /// Type of engine
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Features supported by the engine
        /// </summary>
        public EngineSupports Supports { get; set; }
    }
}