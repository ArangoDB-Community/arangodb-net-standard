using System;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Represents one log entry in ArangoDB
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Unique Id of the log entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Topic or subject of the log entry.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Level of the log entry.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Date and time when the log entry was created.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The logged message.
        /// </summary>
        public string Message { get; set; }
    }
}