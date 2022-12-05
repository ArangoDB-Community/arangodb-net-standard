namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// List of pregel algorithm names
    /// </summary>
    public static class PregelAlgorithms
    {
        /// <summary>
        /// Page Rank
        /// </summary>
        public const string PageRank = "pagerank";

        /// <summary>
        /// Single-Source Shortest Path
        /// </summary>
        public const string SSSP = "sssp";

        /// <summary>
        /// Connected Components
        /// </summary>
        public const string ConnectedComponents = "connectedcomponents";

        /// <summary>
        /// Weakly Connected Components
        /// </summary>
        public const string WCC = "wcc";

        /// <summary>
        /// Strongly Connected Components
        /// </summary>
        public const string SCC = "scc";

        /// <summary>
        /// Hyperlink-Induced Topic Search
        /// </summary>
        public const string HITS = "hits";

        /// <summary>
        /// Effective Closeness
        /// </summary>
        public const string EffectiveCloseness = "effectivecloseness";

        /// <summary>
        /// LineRank
        /// </summary>
        public const string LineRank = "linerank";

        /// <summary>
        /// Label Propagation
        /// </summary>
        public const string LabelPropagation = "labelpropagation";

        /// <summary>
        /// Speaker-Listener Label Propagation
        /// </summary>
        public const string SLPA = "slpa";
    }
}
