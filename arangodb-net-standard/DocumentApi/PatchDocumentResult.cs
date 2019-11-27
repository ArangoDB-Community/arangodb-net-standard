namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentResult<T>
    {
        public T New { get; set; }

        public T Old { get; set; }

        public string _key { get; set; }

        public string _rev { get; set; }

        public string _oldRev { get; set; }

        public string _id { get; set; }
    }
}
