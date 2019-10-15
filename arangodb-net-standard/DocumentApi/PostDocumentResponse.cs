namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Response after posting a single document
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentResponse<T> : DocumentBase
    {
        /// <summary>
        /// Deserialized copy of the new document object. This will only be present if requested with the
        /// <see cref="PostDocumentsOptions.ReturnNew"/> option.
        /// </summary>
        public T New { get; set; }

        /// <summary>
        /// Deserialized copy of the old document object. This will only be present if requested with the
        /// <see cref="PostDocumentsOptions.ReturnOld"/> option.
        /// </summary>
        public T Old { get; set; }
    }

}