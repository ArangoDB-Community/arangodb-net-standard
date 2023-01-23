namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    public class TextAnalyzerEdgeNgram
    {
        /// <summary>
        /// Minimal n-gram length
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Maximal n-gram length
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Whether to include the original token 
        /// even if its length is less than <see cref="Min"/> 
        /// or greater than <see cref="Max"/>
        /// </summary>
        public bool PreserveOriginal { get; set; }
    }
}