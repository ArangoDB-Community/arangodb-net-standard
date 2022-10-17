using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// Interface for <see cref="ApiResponseBase"/>
    /// </summary>
    public class ApiResponseBase:IApiResponseBase
    {
        /// <summary>
        /// Header information from the API
        /// </summary>
        public ApiResponseHeaders ResponseHeaders { get; set; }
    }
}
