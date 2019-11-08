﻿using System.Net;

namespace ArangoDBNetStandard.TransactionApi
{
    /// <summary>
    /// Response from ArangoDB after executing a transaction.
    /// </summary>
    /// <typeparam name="T">Type used to deserialize the returned object from the transaction function.</typeparam>
    public class PostTransactionResponse<T>
    {
        /// <summary>
        /// Whether the request resulted in error.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The ArangoDB result code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Deserialized result from the transaction function.
        /// </summary>
        public T Result { get; set; }
    }
}