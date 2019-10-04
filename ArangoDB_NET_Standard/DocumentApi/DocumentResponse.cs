namespace ArangoDB_NET_Standard.DocumentApi
{
    public class DocumentResponse<T> : DocumentResponseBase
    {
        public T Document { get; private set; }

        public DocumentResponse(T document)
        {
            this.Document = document;
        }
    }
}