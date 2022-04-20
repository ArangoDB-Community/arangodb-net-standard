using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Parameter for <see cref="IDocumentApiClient.PutReadMultipleDocuments(string, PutReadMultipleDocumentsQuery, IList{string}, IList{PutReadMultipleDocumentsBodyItem})"/>
    /// </summary>
    public class PutReadMultipleDocumentsBodyItem
    {
        /// <summary>
        /// Required. Document key to search for.
        /// </summary>
        public string _key { get; set; }

        /// <summary>
        /// Optional. May be specified to verify 
        /// whether the document has the same
        /// revision value.
        /// </summary>
        public string _rev { get; set; }
    }
}