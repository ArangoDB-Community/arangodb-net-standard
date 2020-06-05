namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents a response returned when replacing a single document.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PutDocumentResponse<T> : DocumentBase
    {
        /// <summary>
        /// Contains the revision of the old (now replaced) document.
        /// </summary>
        public string _oldRev { get; set; }

        /// <summary>
        /// Deserialized copy of the new document object. This will only be present if requested with the
        /// <see cref="PutDocumentQuery.ReturnNew"/> option.
        /// </summary>
        public T New { get; set; }

        /// <summary>
        /// Deserialized copy of the old document object. This will only be present if requested with the
        /// <see cref="PutDocumentQuery.ReturnOld"/> option.
        /// </summary>
        public T Old { get; set; }
    }
}
