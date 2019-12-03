using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutEdgeDefinitionBody
    {
        public string Collection { get; set; }

        public IEnumerable<string> From { get; set; }

        public IEnumerable<string> To { get; set; }
    }
}
