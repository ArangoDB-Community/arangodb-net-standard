using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class PostGraphBody
    {
        public string Name { get; set; }

        public List<EdgeDefinition> EdgeDefinitions { get; set; }

        public bool? IsSmart { get; set; }

        public PostGraphOptions Options { get; set; }
    }
}
