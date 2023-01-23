namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Body parameters for <see cref="IViewApiClient.PutRenameViewAsync"/>
    /// </summary>
    public class PutRenameViewBody
    {
        /// <summary>
        /// The new name for the view
        /// </summary>
        public string Name { get; set; }
    }

}
