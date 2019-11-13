using System.Net;

namespace ArangoDBNetStandard.DatabaseApi
{
    public class CurrentDatabaseResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public CurrentDatabaseResult Result { get; set; }
    }
}
