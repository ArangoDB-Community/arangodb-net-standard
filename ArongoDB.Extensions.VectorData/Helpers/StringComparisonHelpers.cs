namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class StringComparisonHelpers
{
    public static string GetComparisonOptionsForLike(
        this StringComparison stringComparison)
    {
        return stringComparison switch
        {
            StringComparison.OrdinalIgnoreCase
                    or StringComparison.CurrentCultureIgnoreCase
                    or StringComparison.InvariantCultureIgnoreCase
                => "true",
            _ => "false"
        };
    }

    public static string? GetComparisonOptionsForContainsOrEquals(
        this StringComparison stringComparison)
    {
        return stringComparison switch
        {
            StringComparison.OrdinalIgnoreCase
                    or StringComparison.CurrentCultureIgnoreCase
                    or StringComparison.InvariantCultureIgnoreCase
                => "LOWER",
            _ => null
        };
    }
}
