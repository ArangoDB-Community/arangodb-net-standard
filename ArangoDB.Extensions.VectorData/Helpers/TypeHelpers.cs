using System.Reflection;

namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class TypeHelpers
{
    public static string[] GetPublicPropertyNamesExcluding(
        this Type type,
        string? excludePropertyName)
    {
        if (type is null)
        {
            return [];
        }

        string[] props = [.. type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !string.Equals(p.Name, excludePropertyName, StringComparison.OrdinalIgnoreCase))
            .Where(p => !p.GetCustomAttributes(true)
                .Any(a => string.Equals(a.GetType().Name, "JsonIgnoreAttribute", StringComparison.Ordinal)))
            .Select(p => p.Name)];

        return props;
    }
}
