using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi
{
    /// <summary>
    /// Additional data relating to a query result cursor.
    /// </summary>
    public class CursorResponseExtra
    {
        /// <summary>
        /// Query ID, can be used to fetch more results from a cursor or to delete a cursor.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Various stats related to the query.
        /// </summary>
        public CursorResponseStats Stats { get; set; }

        /// <summary>
        /// Plan info is only available when the <see cref="PostCursorOptions.Profile"/> option
        /// is set to 2.
        /// </summary>
        public CursorResponsePlan Plan { get; set; }

        /// <summary>
        /// Profile info is only available when the <see cref="PostCursorOptions.Profile"/> option
        /// is set to 1 or 2.
        /// </summary>
        /// <remarks>
        /// The following profile entries are expected:
        /// - "initializing"
        /// - "parsing"
        /// - "optimizing ast"
        /// - "loading collections"
        /// - "instantiating plan"
        /// - "optimizing plan"
        /// - "executing"
        /// - "finalizing"
        /// </remarks>
        public Dictionary<string, double> Profile { get; set; }

        /// <summary>
        /// List of warnings related to the query.
        /// </summary>
        public IEnumerable<CursorResponseWarning> Warnings { get; set; }
    }
}
