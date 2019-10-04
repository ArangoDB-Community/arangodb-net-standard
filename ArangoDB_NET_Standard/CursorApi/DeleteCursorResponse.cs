namespace ArangoDB_NET_Standard.CursorApi
{
    public class DeleteCursorResponse
    {
        public int StatusCode { get; private set; }

        public DeleteCursorResponse(int statusCode)
        {
            this.StatusCode = statusCode;
        }
    }
}