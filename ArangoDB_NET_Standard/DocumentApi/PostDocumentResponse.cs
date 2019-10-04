namespace ArangoDB_NET_Standard.DocumentApi
{
    /// <summary>
    /// Response after posting a single document
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentResponse<T> : PostDocumentResponse
    {
        public T New { get; set; }

        public T Old { get; set; }
    }

    public class PostDocumentResponse
    {
        public string _key { get; set; }

        public string _id { get; set; }

        public string _rev { get; set; }
    }
}