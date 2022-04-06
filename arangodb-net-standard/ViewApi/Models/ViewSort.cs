namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Defines sort order for a view.
    /// Read more about this in the documentation.
    /// </summary>
    public class ViewSort
    {
        /// <summary>
        /// Name of the field to sort by
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Direction of the sort: 
        /// ("asc for ascending, "desc" for descending)
        /// </summary>
        public string Direction { get; set; }
    }

}
