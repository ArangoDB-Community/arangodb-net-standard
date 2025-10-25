using Microsoft.Extensions.VectorData;

namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class FilterRetrievalOptions
{
    public static string BuildSortOrder<TRecord>(
        this FilteredRecordRetrievalOptions<TRecord>.OrderByDefinition.SortInfo info
    ) where TRecord : class
    {
        return info.Ascending ? "ASC" : "DESC";
    }
}
