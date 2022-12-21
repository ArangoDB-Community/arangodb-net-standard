using System;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Response from <see cref="IIndexApiClient.PostInvertedIndexAsync(PostIndexQuery, PostInvertedIndexBody, System.Threading.CancellationToken)"/>
    /// </summary>
    public class InvertedIndexResponse : PostInvertedIndexBody
    {
        public string Id { get; set; }
        public bool? IsNewlyCreated { get; set; }
        public bool? Sparse { get; set; }
        public bool? Unique { get; set; }
        public string Version { get; set; }
        public int? Code { get; set; }
        public bool? Error { get; set; }
    }
}
