using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi
{
    public class CursorResponseExtra
    {
        public string Id { get; set; }

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
    }
}
