namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents information about the current database.
    /// </summary>
    public class CurrentDatabaseInfo
    {
        /// <summary>
        /// The name of the current database.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id of the current database.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The filesystem path of the current database.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Whether or not the current database is the _system database.
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
