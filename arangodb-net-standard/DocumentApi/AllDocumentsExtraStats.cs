namespace ArangoDBNetStandard.DocumentApi
{
    public class AllDocumentsExtraStats
    {
        public int WritesExecuted { get; set; }

        public int WritesIgnored { get; set; }

        public int ScannedFull { get; set; }

        public int ScannedIndex { get; set; }

        public int Filtered { get; set; }

        public int HttpRequests { get; set; }

        public decimal ExecutionTime { get; set; }

        public int PeakMemoryUsage { get; set; }

    }
}
