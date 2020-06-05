using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response returned when replacing multiple document.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PutDocumentsResponse<T> : List<PutDocumentsDocumentResponse<T>>
    {
        /// <summary>
        /// Creates a new instance of <see cref="PutDocumentsResponse{T}"/>
        /// with default values.
        /// </summary>
        public PutDocumentsResponse()
        {
        }

        private PutDocumentsResponse(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Creates an empty response.
        /// </summary>
        /// <returns></returns>
        public static PutDocumentsResponse<T> Empty()
        {
            return new PutDocumentsResponse<T>(0);
        }
    }
}
