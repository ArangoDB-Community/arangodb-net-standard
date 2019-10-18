using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class DeleteDocumentResponse<T>: DocumentBase
    {
        public T Old { get; set; }
    }
}