namespace ArangoDBNetStandard.CollectionApi
{
    public class Figures
    {
        public DataFiles DataFiles { get; set; }

        public int UncollectedLogfileEntries { get; set; }

        public int DocumentReferences { get; set; }

        public CompactionStatus CompactionStatus { get; set; }

        public Compactors Compactors { get; set; }

        public Dead Dead { get; set; }

        public Indexes Indexes { get; set; }

        public ReadCache Readcache { get; set; }

        public string WaitingFor { get; set; }

        public Alive Alive { get; set; }

        public int LastTick { get; set; }

        public Journals Journals { get; set; }

        public Revisions Revisions { get; set; }
    }
}
