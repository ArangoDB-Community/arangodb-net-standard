using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutGraphDefinitionBody
    {
        public string Collection { get; set; }

        public IEnumerable<string> From { get; set; }

        public IEnumerable<string> To { get; set; }
    }
}
