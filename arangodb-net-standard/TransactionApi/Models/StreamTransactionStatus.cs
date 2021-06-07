namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Enum containing the different stream transaction status.
    /// </summary>
    public enum StreamTransactionStatus
    {
        /// <summary>
        /// Transaction is running.
        /// </summary>
        Running,

        /// <summary>
        /// Transaction is committed.
        /// </summary>
        Committed,

        /// <summary>
        /// Transaction is aborted.
        /// </summary>
        Aborted,
    }
}
