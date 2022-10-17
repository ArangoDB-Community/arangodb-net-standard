using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ArangoDBNetStandard
{
    public class ApiResponseHeaders
    {
        public ApiResponseHeaders(HttpResponseHeaders hrh)
        {
            Headers = hrh;
        }

        public HttpResponseHeaders Headers { get; private set; }

        /// <summary>
        /// The time in seconds the request was queued before executed. 
        /// This is returned by the server in all the responses.
        /// </summary>
        public decimal? QueueTimeSeconds 
        { 
            get 
            {
                return GetHeaderDecimal("x-arango-queue-time-seconds");
            }
        }

        decimal? GetHeaderDecimal(string name)
        {
            string v = GetHeaderValue(name);
            if (string.IsNullOrWhiteSpace(v))
            {
                return null;
            }
            else
            {
                return decimal.Parse(v);
            }
        }

        string GetHeaderValue(string name)
        {
            string result = null;
            if (Headers != null && Headers.TryGetValues(name, out IEnumerable<string> values))
            {
                if (values != null && values.Count(x => !string.IsNullOrWhiteSpace(x)) > 0)
                {
                    result = values.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
                }
            }
            return result;
        }
    }
}