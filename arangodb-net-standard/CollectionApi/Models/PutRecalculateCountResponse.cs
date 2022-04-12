namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutRecalculateCountResponse : ResponseBase
    {
        /// <summary>
        /// Indicates if recalculating 
        /// the document count succeeded.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// The number of documents in the
        /// collection
        /// </summary>
        public int Count { get; set; }
    }
}