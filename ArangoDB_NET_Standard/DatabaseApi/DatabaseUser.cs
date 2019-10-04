using System.Collections.Generic;

namespace ArangoDB_NET_Standard.DatabaseApi
{
    public class DatabaseUser
    {
        public string Username { get; set; }

        public string Passwd { get; set; }

        public bool Active { get; set; }

        public Dictionary<string, object> Extra { get; set; }
    }
}