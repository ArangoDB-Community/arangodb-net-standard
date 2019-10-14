using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Response after posting multiple documents
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentsResponse<T>: List<PostDocumentsDocumentResponse<T>>
    {

    }
}