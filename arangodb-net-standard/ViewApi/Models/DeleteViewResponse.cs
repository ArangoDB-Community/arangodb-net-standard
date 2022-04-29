namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Response from <see cref="IViewApiClient.DeleteViewAsync(string)"/>
    /// </summary>
    public class DeleteViewResponse :ResponseBase
    {
        /// <summary>
        /// Indicates whether the delete 
        /// operation was successful.
        /// </summary>
        public bool Result { get; set; }
    }
}
