using System.Collections.Generic;

namespace ArangoDB_NET_Standard.DatabaseApi
{
    public class PostDatabaseRequest
    {
        public string Name { get; set; }

        public IList<DatabaseUser> Users { get; set; }
    }
}