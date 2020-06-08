using System.Net;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Response model for a single POST Document request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentsDocumentResponse<T> : PostDocumentResponse<T>
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }
    }
}