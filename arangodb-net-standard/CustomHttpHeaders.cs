namespace ArangoDBNetStandard
{
    /// <summary>
    /// The custom HttpHeaders that may be specified in a client request.
    /// </summary>
    public static class CustomHttpHeaders
    {
        /// <summary>
        /// The header string used for Stream Transaction.
        /// </summary>    
        public const string StreamTransactionHeader = "x-arango-trx-id";
    }
}
