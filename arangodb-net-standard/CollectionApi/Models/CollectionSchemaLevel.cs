namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Defines the possible collection schema levels.
    /// See https://www.arangodb.com/docs/3.9/data-modeling-documents-schema-validation.html#levels
    /// </summary>
    public static class CollectionSchemaLevel
    {
        /// <summary>
        /// The rule is inactive and validation thus turned off.
        /// </summary>
        public const string None = "none";

        /// <summary>
        /// Only newly inserted documents are validated.
        /// </summary>
        public const string New = "new";

        /// <summary>
        /// New and modified documents must pass validation, except 
        /// for modified documents where the OLD value did not pass 
        /// validation already. This level is useful if you have 
        /// documents which do not match your target structure, but 
        /// you want to stop the insertion of more invalid documents
        /// and prohibit that valid documents are changed to invalid 
        /// documents.
        /// </summary>
        public const string Moderate = "moderate";

        /// <summary>
        /// All new and modified document must strictly pass validation.
        /// No exceptions are made.
        /// This is the default level.
        /// </summary>
        public const string Strict = "strict";
    }
}
