using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi
{
    public class CursorResponsePlan
    {
        public IEnumerable<object> Nodes { get; set; }

        public IEnumerable<string> Rules { get; set; }

        public IEnumerable<CursorResponsePlanCollection> Collections { get; set; }

        public IEnumerable<CursorResponsePlanVariable> Variables { get; set; }

        public long EstimatedCost { get; set; }

        public long EstimatedNrItems { get; set; }

        public bool Initialize { get; set; }

        public bool IsModificationQuery { get; set; }
    }
}
