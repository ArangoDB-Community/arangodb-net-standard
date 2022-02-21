using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public enum IndexType
    {
        FullText,
        Geo,
        Persistent,
        TTL
    }
}
