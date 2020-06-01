using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class DeleteDocumentsResponse<T> : List<DeleteDocumentsDocumentResponse<T>>
    {
        public DeleteDocumentsResponse()
        {
        }

        private DeleteDocumentsResponse(int capacity)
            : base(capacity)
        {
        }

        public static DeleteDocumentsResponse<T> Empty()
        {
            return new DeleteDocumentsResponse<T>(0);
        }
    }
}