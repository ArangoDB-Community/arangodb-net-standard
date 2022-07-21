using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class GetVertexResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public T Vertex { get; set; }

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }
    }
}
