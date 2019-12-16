using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class DeleteDocumentsResponse<T>: List<DeleteDocumentsDocumentResponse<T>>
    {
    }
}