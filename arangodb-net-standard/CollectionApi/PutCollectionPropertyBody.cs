using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace ArangoDBNetStandard.CollectionApi
{
    public class PutCollectionPropertyBody
    {
        public bool? WaitForSync { get; set; }

        public long? JournalSize { get; set; }
        
    }
}