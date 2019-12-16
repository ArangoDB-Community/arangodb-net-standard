namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class PatchDocumentsResponse<U> : DocumentBase
    {
        public U New { get; set; }

        public U Old { get; set; }

        public bool Error { get; set; }

        public int ErrorNum { get; set; }
    }
}
