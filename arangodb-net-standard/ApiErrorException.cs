using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArangoDBNetStandard
{
    [Serializable]
    public class ApiErrorException : Exception
    {
        /// <summary>
        /// Can be null if ArangoDB returns empty content 
        /// in the response.
        /// </summary>
        public ApiErrorResponse ApiError { get; set; }
        public Dictionary<string,string> Headers { get; set; }

        public ApiErrorException()
        {
        }

        public ApiErrorException(ApiErrorResponse error) : base(error == null ? string.Empty : error.ErrorMessage)
        {
            ApiError = error;
        }

        public ApiErrorException(string message) : base(message)
        {
        }

        public ApiErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}