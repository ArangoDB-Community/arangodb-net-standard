namespace ArangoDB.Extensions.VectorData.Helpers;

internal static class KeyHelpers
{
    public static string SanitizeKeyAndGetId<TKey>(this TKey key, string collectionName)
        where TKey : notnull
    {
        if (key is not string keyString)
        {
            keyString = key.ToString();
        }
        if (string.IsNullOrWhiteSpace(keyString))
        {
            throw new ArgumentException("Key can't be null or empty.", nameof(key));
        }
        else if (!keyString.Contains('/'))
        {
            string trimmedKey = keyString.Trim();
            return trimmedKey.Contains(" ")
                ? throw new FormatException("Key cannot contain spaces.")
                : $"{collectionName}/{trimmedKey}";
        }
        else if (keyString.StartsWith("/"))
        {
            return $"{collectionName}{keyString}";
        }
        else if (keyString.EndsWith("/"))
        {
            throw new FormatException("Key string cannot end with a slash.");
        }

        ReadOnlyMemory<string> keyParts = keyString.Trim().Split('/').AsMemory();
        if (keyParts.Length > 2)
        {
            throw new FormatException("The 'Key' can either be the document key or the fully qualified id (CollectionName/DocumentKey).");
        }
        else if (IsCollectionNameMismatch(collectionName, keyParts))
        {
            throw new NotSupportedException("A document from another collection can't be accessed.");
        }

        return keyString.Trim();
    }

    private static bool IsCollectionNameMismatch(string collectionName, ReadOnlyMemory<string> keyParts)
    {
        return keyParts.Length == 2
            && !string.Equals(keyParts.Span[0], collectionName, StringComparison.OrdinalIgnoreCase);
    }
}
