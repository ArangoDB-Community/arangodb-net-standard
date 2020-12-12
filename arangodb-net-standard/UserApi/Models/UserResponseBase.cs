using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a common response class with user information
    /// returned after performing a user operation.
    /// </summary>
    public class UserResponseBase : ResponseBase
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
    }
}
