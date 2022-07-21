using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class PutVertexResponse<T>
    {
        public T New { get; set; }

        public T Old { get; set; }

        public HttpStatusCode Code { get; set; }

        public PutVertexResult Vertex { get; set; }

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
