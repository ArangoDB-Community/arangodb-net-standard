using System;
using ArangoDBNetStandard.AdminApi;
using ArangoDBNetStandard.AnalyzerApi;
using ArangoDBNetStandard.AqlFunctionApi;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.BulkOperationsApi;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.IndexApi;
using ArangoDBNetStandard.PregelApi;
using ArangoDBNetStandard.TransactionApi;
using ArangoDBNetStandard.UserApi;
using ArangoDBNetStandard.ViewApi;

namespace ArangoDBNetStandard
{
    public interface IArangoDBClient : IDisposable
    {
        /// <summary>
        /// AQL user functions management API.
        /// </summary>
        IAqlFunctionApiClient AqlFunction { get; }

        /// <summary>
        /// Auth API
        /// </summary>
        IAuthApiClient Auth { get; }

        /// <summary>
        /// Cursor API
        /// </summary>
        ICursorApiClient Cursor { get; }

        /// <summary>
        /// Database API
        /// </summary>
        IDatabaseApiClient Database { get; }

        /// <summary>
        /// Document API
        /// </summary>
        IDocumentApiClient Document { get; }

        /// <summary>
        /// Collection API
        /// </summary>
        ICollectionApiClient Collection { get; }

        /// <summary>
        /// Transaction API
        /// </summary>
        ITransactionApiClient Transaction { get; }

        /// <summary>
        /// Graph API
        /// </summary>
        IGraphApiClient Graph { get; }

        /// <summary>
        /// User management API
        /// </summary>
        IUserApiClient User { get; }

        /// <summary>
        /// Index management API
        /// </summary>
        IIndexApiClient Index { get; }
        
        /// <summary>
        /// Bulk Operations API
        /// </summary>
        IBulkOperationsApiClient BulkOperations { get; }                        
                       
        /// <summary>
        /// View management API
        /// </summary>
        IViewApiClient View { get; }                    

        /// <summary>
        /// Analyzer managemet API
        /// </summary>
        IAnalyzerApiClient Analyzer { get; }

        /// <summary>
        /// Admin API
        /// </summary>
        IAdminApiClient Admin { get; }

        /// <summary>
        /// Pregel API
        /// </summary>
        IPregelApiClient Pregel { get; }
    }
}