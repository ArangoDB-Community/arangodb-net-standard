using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class Graph
    {
        public string SmartGraphAttribute { get; set; }

        public int ReplicationFactor { get; set; }

        public IEnumerable<string> OrphanCollections { get; set; }

        public string Name { get; set; }

        public string _rev { get; set; }

        public int NumberOfShards { get; set; }

        public bool IsSmart { get; set; }

        public string _id { get; set; }

        public IEnumerable<EdgeDefinition> EdgeDefinitions { get; set; }
    }
}
