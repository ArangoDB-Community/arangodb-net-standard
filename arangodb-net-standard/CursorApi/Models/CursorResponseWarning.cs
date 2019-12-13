namespace ArangoDBNetStandard.CursorApi.Models
{
    public class CursorResponseWarning
    {
        /// <summary>
        /// Error code
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}