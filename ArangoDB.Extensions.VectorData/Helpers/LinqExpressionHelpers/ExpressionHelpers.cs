using System;
using System.Collections;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

internal static class ExpressionHelpers
{
    public static string BuildWhereClause(
        this Expression expression,
        Dictionary<string, object> bindVars)
    {
        return expression switch
        {
            BinaryExpression be => be.BuildBinary(bindVars),
            UnaryExpression ue when ue.NodeType == ExpressionType.Not
                => $"NOT ({ue.Operand.BuildWhereClause(bindVars)})",
            MemberExpression me => me.BuildMemberAccess(),
            ConstantExpression ce => bindVars.AddBindVar(ce.Value),
            MethodCallExpression mce => mce.HandleOperationInFilterCondition(bindVars),
            _ => throw new NotSupportedException($"Unsupported expression: {expression.NodeType}")
        };
    }

    public static string BuildOperand(
        this Expression expr,
        Dictionary<string, object> bindVars,
        MethodCallExpression? mce = null,
        AqlLikeWildcardPositions? wildcardPosition = null)
    {
        switch (expr)
        {
            case MemberExpression me:
                return me.BuildMemberAccess();
            case ConstantExpression ce
                when ce.Value is List<string> strings:
                return bindVars.AddBindVar($"[{string.Join(", ", strings.Select(s => $"\"{s}\""))}]");
            case ConstantExpression ce
                when ce.Value is IEnumerable<string> strings:
                return bindVars.AddBindVar($"[{string.Join(", ", strings.Select(s => $"\"{s}\""))}]");
            case ConstantExpression ce
                when ce.Value is IEnumerable<int> or IEnumerable<float> or IEnumerable<double> or IEnumerable<decimal> or IEnumerable<BigInteger>:
                return bindVars.AddBindVar(JsonSerializer.Serialize(ce.Value));
            case ConstantExpression ce
                when mce is not null
                  && wildcardPosition is null or AqlLikeWildcardPositions.Both:
                return bindVars.AddBindVar($"%{ce.Value}%");
            case ConstantExpression ce
                when mce is not null
                  && wildcardPosition is AqlLikeWildcardPositions.Start:
                return bindVars.AddBindVar($"%{ce.Value}");
            case ConstantExpression ce
                when mce is not null
                  && wildcardPosition is AqlLikeWildcardPositions.End:
                return bindVars.AddBindVar($"{ce.Value}%");
            case ConstantExpression ce:
                return bindVars.AddBindVar(ce.Value);
            default:
                object value = Expression
                    .Lambda(expr)
                    .Compile()
                    .DynamicInvoke();
                return bindVars.AddBindVar(value);
        }
    }

    public static string BuildOperand(
        this Expression<string> expr,
        Dictionary<string, object> bindVars)
    {
        string value = expr.Compile();
        return bindVars.AddBindVar(value);
    }
}
