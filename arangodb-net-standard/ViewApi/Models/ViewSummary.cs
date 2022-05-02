namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Provides summary info about a view
    /// </summary>
    public class ViewSummary
    {
        /// <summary>
        /// The globally unique identifier of the View
        /// </summary>
        public string GloballyUniqueId { get; set; }

        /// <summary>
        /// The identifier of the View
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the View
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the View 
        /// </summary>
        public string Type { get; set; }
    }
}