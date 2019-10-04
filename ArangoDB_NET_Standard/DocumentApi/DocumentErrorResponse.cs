namespace ArangoDB_NET_Standard.DocumentApi
{
    internal class DocumentErrorResponse : DocumentResponseBase
    {
        public int StatusCode { get; private set; }

        public DocumentErrorResponse(int statusCode)
        {
            this.StatusCode = statusCode;
        }
    }
}