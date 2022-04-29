using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Properties for linked attribute value
    /// <see cref="ViewDetails.Links"/>
    /// </summary>
    public class LinkProperties
    {
        /// <summary>
        /// Possible value for <see cref="StoreValues"/>
        /// Store information about value presence to
        /// allow use of the EXISTS() function.
        /// </summary>
        public const string NoStoreValue = "none";

        /// <summary>
        /// Possible value for <see cref="StoreValues"/>
        /// Do not store value meta data in the View.
        /// </summary>
        public const string IdStoreValue = "id";

        /// <summary>
        /// A list of analyzers, by name as defined 
        /// via the Analyzers feature, that should 
        /// be applied to values of processed 
        /// document attributes.
        /// </summary>
        public List<string> Analyzers { get; set; }

        /// <summary>
        /// Fields that should be processed at each
        /// level of the document. Each key specifies
        /// the document attribute/field to be processed.
        /// Note that the value of includeAllFields is 
        /// also consulted when selecting fields to be 
        /// processed. Each value specifies the Link 
        /// properties directives to be used when processing
        /// the specified field, an empty/null value denotes
        /// inheritance of all (except fields) directives 
        /// from the current level.
        /// </summary>
        public IDictionary<string, LinkProperties> Fields { get; set; }

        /// <summary>
        /// If set to true, then process all document attributes.
        /// Otherwise, only consider attributes mentioned in fields.
        /// Attributes not explicitly specified in fields will be
        /// processed with default link properties, i.e. null.
        /// Using includeAllFields for a lot of attributes in
        /// combination with complex Analyzers may significantly
        /// slow down the indexing process.
        /// </summary>
        public bool IncludeAllFields { get; set; }

        /// <summary>
        /// If set to true, then for array values track the value
        /// position in arrays. E.g., when querying for the input
        /// { attr: [ "valueX", "valueY", "valueZ" ] }, the user
        /// must specify: doc.attr[1] == "valueY". Otherwise, all
        /// values in an array are treated as equal alternatives.
        /// E.g., when querying for the input
        /// { attr: [ "valueX", "valueY", "valueZ" ] }, the user 
        /// must specify: doc.attr == "valueY"
        /// </summary>
        public bool TrackListPositions { get; set; }

        /// <summary>
        /// Controls how the view should keep track of the
        /// attribute values. Valid values are 
        /// <see cref="NoStoreValue"/> and <see cref="NoStoreValue"/>.
        /// Not to be confused with <see cref="ViewDetails.StoredValues"/>, 
        /// which stores attribute values in the View index.
        /// </summary>
        public string StoreValues { get; set; }

        /// <summary>
        /// If set to true, then no exclusive lock is used on the
        /// source collection during View index creation, so that 
        /// it remains basically available. This option can be set
        /// when adding links. It does not get persisted as it is 
        /// not a View property, but only a one-off option.
        /// </summary>
        public bool InBackground { get; set; }
    }
}