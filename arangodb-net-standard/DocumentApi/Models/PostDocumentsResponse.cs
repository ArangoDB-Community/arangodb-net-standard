using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response for creating multiple documents.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PostDocumentsResponse<T> : List<PostDocumentsDocumentResponse<T>>
    {
        /// <summary>
        /// Creates an instance of <see cref="PostDocumentsResponse{T}"/>.
        /// </summary>
        public PostDocumentsResponse()
        {
        }

        private PostDocumentsResponse(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Creates an empty response.
        /// This is used when <see cref="PostDocumentsQuery.Silent"/> is true.
        /// </summary>
        /// <returns></returns>
        public static PostDocumentsResponse<T> Empty()
        {
            return new PostDocumentsResponse<T>(0);
        }
    }
}