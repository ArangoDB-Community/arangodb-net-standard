namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Defines sort order for a view.
    /// Read more about this in the documentation.
    /// </summary>
    public class ViewSort
    {
        /// <summary>
        /// Possible value for <see cref="Direction"/>
        /// </summary>
        public const string AscendingSortDirection = "asc";

        /// <summary>
        /// Possible value for <see cref="Direction"/>
        /// </summary>
        public const string DescendingSortDirection = "desc";

        /// <summary>
        /// Name of the field to sort by
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Direction of the sort: 
        /// (<see cref="AscendingSortDirection"/> for ascending, 
        /// <see cref="DescendingSortDirection"/> for descending)
        /// </summary>
        public string Direction { get; set; }
    }

}
