using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class EdgeDefinition
    {
        public IEnumerable<string> To { get; set; }

        public IEnumerable<string> From { get; set; }

        public string Collection { get; set; }
    }
}
