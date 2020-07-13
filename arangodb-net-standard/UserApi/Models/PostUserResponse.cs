using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.UserApi.Models
{
    public class PostUserResponse
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Whether the user is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Object with arbitrary extra data about the user.
        /// </summary>
        public Dictionary<string, object> Extra { get; set; }

        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
