namespace ArangoDBNetStandard.DatabaseApi
{
    public class DeleteDatabaseResponse
    {
        /// <summary>
        /// HttpStatus
        /// </summary>
        public int HttpStatus { get; private set; }

        /// <summary>
        /// True if the database was deleted, otherwise see <see cref="HttpStatus"/>
        /// </summary>
        public bool Result { get; private set; }

        public DeleteDatabaseResponse(int statusCode)
        {
            HttpStatus = statusCode;
            Result = statusCode >= 200 && statusCode < 300;
        }
    }
}