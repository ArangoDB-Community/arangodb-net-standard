using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class GetAllCollectionIndexesResponse : ResponseBase
    {
        public IEnumerable<IndexResponseBase> Indexes { get; set; }
        public Dictionary<string, IndexResponseBase> Identifiers { get; set; }
    }
}
