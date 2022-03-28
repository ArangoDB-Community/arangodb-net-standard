namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class ViewResponse : ViewDetails
    {
        /// <summary>
        /// The globally unique identifier of the View
        /// </summary>
        public string GloballyUniqueId { get; set; }
    }
}
