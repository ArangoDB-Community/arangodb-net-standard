namespace ArangoDB_NET_Standard.DatabaseApi
{
    public class DeleteDatabaseResult
    {
        /// <summary>
        /// HttpStatus
        /// </summary>
        public int HttpStatus { get; private set; }

        /// <summary>
        /// True if the database was deleted, otherwise see <see cref="HttpStatus"/>
        /// </summary>
        public bool Result { get; private set; }

        public DeleteDatabaseResult(int statusCode)
        {
            HttpStatus = statusCode;
            Result = statusCode >= 200 && statusCode < 300;
        }
    }
}