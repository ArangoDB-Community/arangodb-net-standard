using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from
    /// <see cref="AqlFunctionApiClient.GetSlowAqlQueriesAsync(GetSlowAqlQueriesQuery)"/>
    /// </summary>
    public class GetSlowAqlQueriesResponse : List<GetSlowAqlQueriesResponseResult>
    {

    }
}
