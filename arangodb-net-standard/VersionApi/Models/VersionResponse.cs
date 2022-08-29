using System;
using System.Net;

namespace ArangoDBNetStandard.VersionApi.Models
{
    public class VersionResponse
    {
        public string Server { get; set; }

        public string License { get; set; }
        public Version Version { get; set; }
    }
}
