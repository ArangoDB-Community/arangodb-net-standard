namespace ArangoDBNetStandard.CollectionApi
{
    public class DeleteCollectionResponse
    {
        public int StatusCode { get; private set; }

        public DeleteCollectionResponse(int statusCode)
        {
            this.StatusCode = statusCode;
        }
    }
}