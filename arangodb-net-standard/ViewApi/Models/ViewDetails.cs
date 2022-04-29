using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Detailed properties of a view
    /// </summary>
    public class ViewDetails
    {
        /// <summary>
        /// Possible value for <see cref="Type"/>
        /// </summary>
        public const string ArangoSearchViewType = "arangosearch";

        /// <summary>
        /// Possible value for <see cref="PrimarySortCompression"/>
        /// </summary>
        public const string LZ4SortCompression = "lz4";

        /// <summary>
        /// Possible value for <see cref="PrimarySortCompression"/>
        /// </summary>
        public const string NoSortCompression = "none";

        /// <summary>
        /// The name of the View.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the View. 
        /// Must be set to <see cref="ArangoSearchViewType"/>
        /// when creating a view.
        /// This option is immutable.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The number of commits between 
        /// removing unused files in
        /// the ArangoSearch data directory 
        /// (default: 2, to disable use: 0).
        /// Read more about this in the documentation.
        /// </summary>
        public int CleanupIntervalStep { get; set; }

        /// <summary>
        /// The number of milliseconds to wait
        /// between committing View data store 
        /// changes and making documents visible
        /// to queries (default: 1000, to disable use: 0)
        /// Read more about this in the documentation.
        /// </summary>
        public int CommitIntervalMsec { get; set; }

        /// <summary>
        /// The number of milliseconds to wait
        /// between applying ‘consolidationPolicy’ 
        /// to consolidate View data store and 
        /// possibly release space on the filesystem
        /// (default: 10000, to disable use: 0). 
        /// Read more about this in the documentation.
        /// </summary>
        public int ConsolidationIntervalMsec { get; set; }

        /// <summary>
        /// The consolidation policy to apply
        /// for selecting which segments should be merged.
        /// Read more about this in the documentation.
        /// </summary>
        public ViewConsolidationPolicy ConsolidationPolicy { get; set; }

        /// <summary>
        /// A primary sort order can be defined 
        /// to enable an AQL optimization.
        /// Read more about this in the documentation.
        /// </summary>
        public List<ViewSort> PrimarySort { get; set; }

        /// <summary>
        /// Defines how to compress the primary sort data 
        /// (introduced in v3.7.1). ArangoDB v3.5 and v3.6 
        /// always compress the index using LZ4. 
        /// This option is immutable. Possible values:
        /// 1) <see cref="LZ4SortCompression"/> (default): use LZ4 fast compression.
        /// 2) <see cref="NoSortCompression"/>: disable compression to trade space for speed.
        /// Read more about this in the documentation.
        /// </summary>
        public string PrimarySortCompression { get; set; }

        /// <summary>
        /// An array of objects to describe which document 
        /// attributes to store in the View index (introduced in v3.7.1).
        /// It can then cover search queries, which means 
        /// the data can be taken from the index directly
        /// and accessing the storage engine can be avoided.
        /// </summary>
        public List<ViewStoredValue> StoredValues { get; set; }

        /// <summary>
        /// Maximum number of concurrent active
        /// writers (segments) that perform a transaction. 
        /// Other writers (segments) wait till current 
        /// active writers (segments) finish 
        /// (default: 0, use 0 to disable, immutable)
        /// </summary>
        public int? WritebufferActive { get; set; }

        /// <summary>
        /// Maximum number of writers (segments) 
        /// cached in the pool 
        /// (default: 64, use 0 to disable, immutable)
        /// </summary>
        public int? WritebufferIdle { get; set; }

        /// <summary>
        /// Maximum memory byte size per writer 
        /// (segment) before a writer (segment) 
        /// flush is triggered. 
        /// </summary>
        public int? WritebufferSizeMax { get; set; }

        /// <summary>
        /// Expects an object with the attribute keys 
        /// being names of to be linked collections, 
        /// and the link properties as attribute values.
        /// </summary>
        public IDictionary<string, LinkProperties> Links { get; set; }
    }
}