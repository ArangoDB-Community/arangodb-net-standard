namespace ArangoDBNetStandard.CollectionApi
{
    public class CompactionStatus
    {
        public string Message { get; set; }

        public string Time { get; set; }

        public int Count { get; set; }

        public int FilesCombined { get; set; }

        public int BytesRead { get; set; }

        public int BytesWritten { get; set; }
    }
}
