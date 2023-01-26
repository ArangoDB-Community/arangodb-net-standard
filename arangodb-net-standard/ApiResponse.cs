using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// Represents a generic response from ArangoDB server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Header values returned by the server
        /// </summary>
        public Dictionary<string,string> Headers { get; set; }

        /// <summary>
        /// The response object returned by the server
        /// </summary>
        public T Response { get; set; }
    }
}