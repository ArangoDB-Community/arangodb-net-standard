using System.Collections.Generic;

namespace ArangoDBNetStandard.DatabaseApi
{
    public class PostDatabaseRequest
    {
        public string Name { get; set; }

        public IList<DatabaseUser> Users { get; set; }
    }
}
