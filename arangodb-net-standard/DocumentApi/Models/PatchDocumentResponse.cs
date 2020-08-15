namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents a response returned when updating a single document.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PatchDocumentResponse<T> : DocumentBase
    {
        /// <summary>
        /// Deserialized copy of the new document object. This will only be present if requested with the
        /// <see cref="PatchDocumentQuery.ReturnNew"/> option.
        /// </summary>
        public T New { get; set; }

        /// <summary>
        /// Deserialized copy of the old document object. This will only be present if requested with the
        /// <see cref="PatchDocumentQuery.ReturnOld"/> option.
        /// </summary>
        public T Old { get; set; }

        /// <summary>
        /// Contains the revision of the old (now updated) document.
        /// </summary>
        public string _oldRev { get; set; }
    }
}
