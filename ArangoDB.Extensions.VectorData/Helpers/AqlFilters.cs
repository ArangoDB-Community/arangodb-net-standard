namespace ArangoDB.Extensions.VectorData.Helpers;

public static class AqlFilters
{
    public static bool Like(this string str, string otherString)
    {
        throw new NotImplementedException();
    }


    public static bool Like(
        this string str, 
        string otherString,
        AqlLikeWildcardPositions wildcardPosition)
    {
        throw new NotImplementedException();
    }

    public static bool Like(
        this string str, 
        string otherString, 
        StringComparison stringComparison)
    {
        throw new NotImplementedException();
    }

    public static bool Like(
        this string str, 
        string otherString, 
        StringComparison stringComparison,
        AqlLikeWildcardPositions wildcardPosition)
    {
        throw new NotImplementedException();
    }
}
