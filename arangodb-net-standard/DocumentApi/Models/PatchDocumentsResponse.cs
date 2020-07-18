using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response for updating multiple documents.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PatchDocumentsResponse<T> : List<PatchDocumentsDocumentResponse<T>>
    {
        /// <summary>
        /// Creates an instance of <see cref="PatchDocumentsResponse{T}"/>.
        /// </summary>
        public PatchDocumentsResponse()
        {
        }

        private PatchDocumentsResponse(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Creates an empty response.
        /// This is used when <see cref="PostDocumentsQuery.Silent"/> is true.
        /// </summary>
        /// <returns></returns>
        public static PatchDocumentsResponse<T> Empty()
        {
            return new PatchDocumentsResponse<T>(0);
        }
    }
}
