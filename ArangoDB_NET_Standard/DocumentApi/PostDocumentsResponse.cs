using System.Collections.Generic;

namespace ArangoDB_NET_Standard.DocumentApi
{
    /// <summary>
    /// Response after posting multiple documents
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentsResponse<T>: List<PostDocumentsDocumentResponse<T>>
    {
    }
}