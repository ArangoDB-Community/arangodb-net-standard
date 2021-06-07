namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Transaction Properties.
    /// </summary>
    public sealed class Transaction
    {
        /// <summary>
        /// Gets or sets the transaction Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        public StreamTransactionStatus State { get; set; }
    }
}
