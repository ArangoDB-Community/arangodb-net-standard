using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostGraphBody
    {
        public string Name { get; set; }

        public List<EdgeDefinition> EdgeDefinitions { get; set; }

        public bool IsSmart { get; set; }

        public PostGraphQuery Options { get; set; }
    }
}
