using System.Collections.Generic;

namespace ArangoDBNetStandard.AdminApi.Models
{
    public class EngineSupports 
    {
        public bool DFDB { get; set; }
        public IList<string> Indexes { get; set; }
        public EngineAlias Aliases { get; set; }
    }
}