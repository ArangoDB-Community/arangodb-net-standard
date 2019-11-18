using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi
{
    public class AllDocumentsExtra
    {
        public AllDocumentsExtraStats Stats { get; set; }

        public IEnumerable<string> Warning { get; set; }
    }
}
