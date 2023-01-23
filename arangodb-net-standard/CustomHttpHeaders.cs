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
        
        /// <summary>
        /// The header string used for Allowing Read From Followers (dirty-reads)
        /// Introduced in ArangoDB 3.10.
        /// </summary>    
        public const string ReadFromFollowersHeader = "x-arango-allow-dirty-read";

        /// <summary>
        /// The header string used for <see cref="ApiHeaderProperties.QueueTimeLimit"/> 
        /// </summary>    
        public const string QueueTimeLimitHeader = "x-arango-max-queue-time-seconds";        

        /// <summary>
        /// The header string used for Driver Info Header
        /// </summary>    
        public const string DriverInfoHeader = "x-arango-driver";
    }
}