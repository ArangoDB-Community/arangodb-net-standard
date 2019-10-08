namespace ArangoDBNetStandard.DocumentApi
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