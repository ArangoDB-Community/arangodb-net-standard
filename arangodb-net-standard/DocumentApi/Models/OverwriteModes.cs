using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{   
    /// <summary>
     /// Defines values for document overwrite modes
     /// Possible value for <see cref="PostDocumentsQuery.OverwriteMode"/>
     /// </summary>
    public static class OverwriteModes
    {
        /// <summary>
        /// If a document with the specified _key value exists already,
        /// nothing is done and no write operation is carried out.
        /// The insert operation returns success in this case. 
        /// This mode does not support returning the old document
        /// version using <see cref="PostDocumentsQuery.ReturnOld"/>. 
        /// When using <see cref="PostDocumentsQuery.ReturnNew"/>, null 
        /// is returned in case the document already existed.
        /// </summary>
        public const string Ignore = "ignore";

        /// <summary>
        /// If a document with the specified _key value exists
        /// already, it is overwritten with the specified document
        /// value. This mode is also used when no overwrite mode
        /// is specified but the <see cref="PostDocumentsQuery.Overwrite"/> 
        /// flag is set to true.
        /// </summary>
        public const string Replace = "replace";

        /// <summary>
        /// If a document with the specified _key value exists
        /// already, it is patched (partially updated) with the
        /// specified document value. The overwrite mode can be 
        /// further controlled via the 
        /// <see cref="PostDocumentsQuery.KeepNull"/> and 
        /// <see cref="PostDocumentsQuery.MergeObjects"/>
        /// parameters.
        /// </summary>
        public const string Update = "update";

        /// <summary>
        /// If a document with the specified _key value exists 
        /// already, return a unique constraint violation error
        /// so that the insert operation fails. This is also 
        /// the default behavior in case the overwrite mode is
        /// not set, and the <see cref="PostDocumentsQuery.Overwrite"/>
        /// flag is false or not set either.
        /// </summary>
        public const string Conflict = "conflict";
    }
}