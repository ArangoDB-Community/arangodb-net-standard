namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a request body to set database or collection access level.
    /// </summary>
    public class PutAccessLevelBody
    {
        /// <summary>
        /// The access level to set.
        /// Use "rw" to set the collection level access to 'Read/Write'.
        /// Use "ro" to set the collection level access to 'Read Only'.
        /// Use "none" to set the collection level access to 'No access'.
        /// </summary>
        public string Grant { get; set; }
    }
}
