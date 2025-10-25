using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

internal static class BinaryExpressionHelpers
{
    public static string BuildBinary(
        this BinaryExpression be,
        Dictionary<string, object> bindVars)
    {
        string op = be.NodeType switch
        {
            ExpressionType.Equal => "==",
            ExpressionType.NotEqual => "!=",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            ExpressionType.AndAlso => "&&",
            ExpressionType.OrElse => "||",
            _ => throw new NotSupportedException($"Unsupported binary operator: {be.NodeType}")
        };

        if (be.NodeType == ExpressionType.AndAlso || be.NodeType == ExpressionType.OrElse)
        {
            string leftLogic = be.Left.BuildWhereClause(bindVars);
            string rightLogic = be.Right.BuildWhereClause(bindVars);
            return $"({leftLogic}) {op} ({rightLogic})";
        }

        string left = be.Left.BuildOperand(bindVars);
        string right = be.Right.BuildOperand(bindVars);
        return $"({left} {op} {right})";
    }
}
