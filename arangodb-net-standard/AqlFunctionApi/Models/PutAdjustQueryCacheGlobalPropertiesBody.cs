namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Parameter to
    /// <see cref="AqlFunctionApiClient.PutAdjustQueryCacheGlobalPropertiesAsync(PutAdjustQueryCacheGlobalPropertiesBody)"/>
    /// </summary>
    public class PutAdjustQueryCacheGlobalPropertiesBody
    {
        public QueryCacheGlobalProperties Properties { get; set; }
    }

}
