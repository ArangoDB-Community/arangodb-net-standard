namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents a response returned when updating a single document.
    /// </summary>
    /// <typeparam name="U">The type of the deserialized new/old document object when requested.</typeparam>
    public class PatchDocumentResponse<U> : DocumentBase
    {
        /// <summary>
        /// Deserialized copy of the new document object. This will only be present if requested with the
        /// <see cref="PatchDocumentQuery.ReturnNew"/> option.
        /// </summary>
        public U New { get; set; }

        /// <summary>
        /// Deserialized copy of the old document object. This will only be present if requested with the
        /// <see cref="PatchDocumentQuery.ReturnOld"/> option.
        /// </summary>
        public U Old { get; set; }

        /// <summary>
        /// Contains the revision of the old (now updated) document.
        /// </summary>
        public string _oldRev { get; set; }
    }
}
