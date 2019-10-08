using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// ArangoDB API error model
    /// </summary>
    public class ApiErrorResponse
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }

        public int Code { get; set; }
    }
}
