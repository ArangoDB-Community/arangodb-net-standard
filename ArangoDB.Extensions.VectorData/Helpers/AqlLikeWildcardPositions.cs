namespace ArangoDB.Extensions.VectorData.Helpers;

public enum AqlLikeWildcardPositions : byte
{
    Both = 0 << 1,
    Start = 1 << 1,
    End = 2 << 1,
}
