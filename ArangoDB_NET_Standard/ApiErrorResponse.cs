using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDB_NET_Standard
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
