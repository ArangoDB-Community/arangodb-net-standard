using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class ViewDetails
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object Links { get; set; }
        public List<ViewSort> PrimarySort { get; set; }
        public string PrimarySortCompression { get; set; }
        public List<ViewStoredValue> StoredValues { get; set; }
        public int? CleanupIntervalStep { get; set; }
        public int? CommitIntervalMsec { get; set; }
        public int? ConsolidationIntervalMsec { get; set; }
        public ViewConsolidationPolicy ConsolidationPolicy { get; set; }
        public int? WritebufferIdle { get; set; }
        public int? WritebufferActive { get; set; }
        public int? WritebufferSizeMax { get; set; }
    }

}
