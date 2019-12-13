using System.Collections.Generic;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    public class PostDatabaseBody
    {
        public string Name { get; set; }

        public IEnumerable<DatabaseUser> Users { get; set; }
    }
}
