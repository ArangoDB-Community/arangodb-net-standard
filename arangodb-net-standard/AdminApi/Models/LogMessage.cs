using System;

namespace ArangoDBNetStandard.AdminApi.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Level { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }

}
