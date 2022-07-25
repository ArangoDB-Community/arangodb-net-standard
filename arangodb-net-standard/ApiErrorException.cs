using System;
using System.Runtime.Serialization;

namespace ArangoDBNetStandard
{
    [Serializable]
    public class ApiErrorException : Exception
    {
        public ApiErrorResponse ApiError { get; set; }

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