namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a response returned after fetching
    /// a database or collection access level.
    /// </summary>
    public class GetAccessLevelResponse : ResponseBase
    {
        /// <summary>
        /// The access level for the specified database or collection.
        /// </summary>
        public string Result { get; set; }
    }
}
