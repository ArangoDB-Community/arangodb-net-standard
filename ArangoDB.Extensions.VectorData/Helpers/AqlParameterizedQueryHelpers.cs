namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class AqlParameterizedQueryHelpers
{
    public static string AddBindVar(
        this Dictionary<string, object> bindVars,
        object? value)
    {
        string name = $"p{bindVars.Count}";
        bindVars[name] = value!;
        return $"@{name}";
    }
}
