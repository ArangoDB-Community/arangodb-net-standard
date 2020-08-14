using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response for deleting multiple documents.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized old document object when requested.</typeparam>
    public class DeleteDocumentsResponse<T> : List<DeleteDocumentsDocumentResponse<T>>
    {
        /// <summary>
        /// Creates an instance of <see cref="DeleteDocumentsResponse{T}"/>.
        /// </summary>
        public DeleteDocumentsResponse()
        {
        }

        private DeleteDocumentsResponse(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Creates an empty response.
        /// This is used when <see cref="DeleteDocumentsQuery.Silent"/> is true.
        /// </summary>
        /// <returns></returns>
        public static DeleteDocumentsResponse<T> Empty()
        {
            return new DeleteDocumentsResponse<T>(0);
        }
    }
}