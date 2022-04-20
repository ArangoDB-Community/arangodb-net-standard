using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class PutReadMultipleDocumentsQuery
    {
        /// <summary>
        /// If this is set to true, if a search 
        /// document contains a value for the field 
        /// <see cref="PutReadMultipleDocumentsBodyItem._rev"/>
        /// then the document is only returned 
        /// if it has the same revision value. 
        /// Otherwise a precondition failed error
        /// is returned.
        /// </summary>
        public bool? IgnoreRevs { get; set; }

        /// <summary>
        /// Get the set of options in a format suited to a URL query string.
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            //onlyget is always true
            query.Add("onlyget=true"); 
            if (IgnoreRevs != null)
            {
                query.Add("ignoreRevs=" + IgnoreRevs.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}