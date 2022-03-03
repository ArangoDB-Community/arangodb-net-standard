using System;
using ArangoDBNetStandard.AqlFunctionApi;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.IndexApi;
using ArangoDBNetStandard.TransactionApi;
using ArangoDBNetStandard.UserApi;

namespace ArangoDBNetStandard
{
    public interface IArangoDBClient : IDisposable
    {
        /// <summary>
        /// AQL user functions management API.
        /// </summary>
        AqlFunctionApiClient AqlFunction { get; }

        /// <summary>
        /// Auth API
        /// </summary>
        AuthApiClient Auth { get; }

        /// <summary>
        /// Cursor API
        /// </summary>
        CursorApiClient Cursor { get; }

        /// <summary>
        /// Database API
        /// </summary>
        DatabaseApiClient Database { get; }

        /// <summary>
        /// Document API
        /// </summary>
        DocumentApiClient Document { get; }

        /// <summary>
        /// Collection API
        /// </summary>
        CollectionApiClient Collection { get; }

        /// <summary>
        /// Transaction API
        /// </summary>
        TransactionApiClient Transaction { get; }

        /// <summary>
        /// Graph API
        /// </summary>
        GraphApiClient Graph { get; }

        /// <summary>
        /// User management API.
        /// </summary>
        UserApiClient User { get; }

        /// <summary>
        /// Index management API.
        /// </summary>
        IndexApiClient Index { get; }
    }
}
