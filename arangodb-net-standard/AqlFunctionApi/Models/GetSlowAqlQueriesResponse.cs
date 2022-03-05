using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    ///// <summary>
    ///// Response from
    ///// <see cref="AqlFunctionApiClient.GetSlowAqlQueriesAsync(GetSlowAqlQueriesQuery)"/>
    ///// </summary>
    //public class GetSlowAqlQueriesResponse:ResponseBase
    //{
    //    /// <summary>
    //    /// An array containing the last AQL queries that are finished and 
    //    /// have exceeded the slow query threshold in the selected database. 
    //    /// The maximum amount of queries in the list can be controlled by 
    //    /// setting the query tracking property maxSlowQueries. 
    //    /// The threshold for treating a query as slow can be adjusted by 
    //    /// setting the query tracking property slowQueryThreshold.
    //    /// </summary>
    //    public IList<GetSlowAqlQueriesResponseResult> Results { get; set; }
    //}


    /// <summary>
    /// Response from
    /// <see cref="AqlFunctionApiClient.GetSlowAqlQueriesAsync(GetSlowAqlQueriesQuery)"/>
    /// </summary>
    public class GetSlowAqlQueriesResponse : List<GetSlowAqlQueriesResponseResult>
    {
        ///// <summary>
        ///// An array containing the last AQL queries that are finished and 
        ///// have exceeded the slow query threshold in the selected database. 
        ///// The maximum amount of queries in the list can be controlled by 
        ///// setting the query tracking property maxSlowQueries. 
        ///// The threshold for treating a query as slow can be adjusted by 
        ///// setting the query tracking property slowQueryThreshold.
        ///// </summary>
        //public IList<GetSlowAqlQueriesResponseResult> Results { get; set; }
    }
}
