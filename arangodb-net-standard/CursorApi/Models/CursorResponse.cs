using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Response from ArangoDB when creating a new cursor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CursorResponse<T> : CursorResponseBase, ICursorResponse<T>
    {
        /// <summary>
        /// Result documents (might be empty if query has no results).
        /// </summary>
        public IEnumerable<T> Result { get; set; }
    }
}
