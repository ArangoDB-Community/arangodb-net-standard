using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostGraphEdgeBody
    {
        public IEnumerable<string> To { get; set; }

        public IEnumerable<string> From { get; set; }

        public string Collection { get; set; }
    }
}
