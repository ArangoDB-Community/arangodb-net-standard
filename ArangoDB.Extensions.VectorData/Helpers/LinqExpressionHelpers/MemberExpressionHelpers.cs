using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

internal static class MemberExpressionHelpers
{
    public static string BuildMemberAccess(
        this MemberExpression me)
    {
        Stack<string> parts = new();
        Expression? current = me;
        while (current is MemberExpression m)
        {
            parts.Push(m.Member.Name);
            current = m.Expression;
        }
        return $"doc.{string.Join(".", parts)}";
    }
}
