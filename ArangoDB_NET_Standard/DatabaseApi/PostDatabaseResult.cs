namespace ArangoDB_NET_Standard.DatabaseApi
{
    public class PostDatabaseResult
    {
        /// <summary>
        /// True if the database was created, otherwise see <see cref="HttpStatus"/>.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int HttpStatus { get; set; }

        public PostDatabaseResult(int httpStatus)
        {
            HttpStatus = httpStatus;
            Result = httpStatus >= 200 && httpStatus < 300;
        }
    }
}