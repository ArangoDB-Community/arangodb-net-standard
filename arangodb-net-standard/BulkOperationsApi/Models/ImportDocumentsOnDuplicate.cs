namespace ArangoDBNetStandard.BulkOperationsApi.Models
{
    /// <summary>
    /// Enum representing possible actions to carry out
    /// in case of a unique key constraint violation.
    /// </summary>
    public enum ImportDocumentsOnDuplicate
    {
        /// <summary>
        /// Will not import the current document 
        /// because of the unique key constraint
        /// violation. This is the default setting.
        /// </summary>
        Error,

        /// <summary>
        /// Will update an existing document in the 
        /// database with the data specified in the
        /// request. Attributes of the existing 
        /// document that are not present in the 
        /// request will be preserved.
        /// </summary>
        Update,

        /// <summary>
        /// Will replace an existing document in the 
        /// database with the data specified in the
        /// request.
        /// </summary>
        Replace,

        /// <summary>
        /// Will not update an existing document and 
        /// simply ignore the error caused by the 
        /// unique key constraint violation.
        /// </summary>
        Ignore
    }
}
