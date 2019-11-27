namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocument<T> : DocumentBase
    {
        public T New { get; set; }

        public T Old { get; set; }

        public bool Error { get; set; }

        public int ErrorNum { get; set; }
    }
}
