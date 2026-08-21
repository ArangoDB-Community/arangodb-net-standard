using ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

using Microsoft.Extensions.VectorData;

namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class SortOperationHelpers
{
    public static string BuildOrderByClause<TRecord>(
       this FilteredRecordRetrievalOptions<TRecord>.OrderByDefinition? orderByDefinition
    ) where TRecord : class
    {
        if (orderByDefinition is null || orderByDefinition.Values.Count == 0)
        {
            return string.Empty;
        }

        List<string> fields =
        [
            ..orderByDefinition.Values
                .Select(info => $"{info.PropertySelector.BuildMemberAccessPath()} {info.BuildSortOrder()}")
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .Select(path => path!)
        ];
        string commaSeparatedFields = string.Join(", ", fields);
        return commaSeparatedFields;
    }
}
