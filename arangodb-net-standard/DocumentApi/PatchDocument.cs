namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocument<U> : DocumentBase
    {
        public U New { get; set; }

        public U Old { get; set; }

        public bool Error { get; set; }

        public int ErrorNum { get; set; }
    }
}
