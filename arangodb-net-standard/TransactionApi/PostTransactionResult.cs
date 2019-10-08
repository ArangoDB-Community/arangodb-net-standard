namespace ArangoDBNetStandard.TransactionApi
{
    public class PostTransactionResponse<T>
    {
        public bool Error { get; set; }

        public int Code { get; set; }

        public T Result { get; set; }
    }
}