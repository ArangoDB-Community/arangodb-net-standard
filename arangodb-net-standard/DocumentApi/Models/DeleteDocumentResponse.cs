namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class DeleteDocumentResponse<T>: DocumentBase
    {
        public T Old { get; set; }
    }
}