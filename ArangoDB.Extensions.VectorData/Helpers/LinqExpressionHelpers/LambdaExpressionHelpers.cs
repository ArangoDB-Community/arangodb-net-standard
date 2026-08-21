using System.Linq.Expressions;
using System.Numerics;

namespace ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

internal static class LambdaExpressionHelpers
{
    public static string? BuildMemberAccessPath(
        this LambdaExpression? lambda)
    {
        return lambda is null
            ? null
            : lambda.Body is MemberExpression memberExpression
              ? memberExpression.BuildMemberAccess()
              : null;
    }

    public static string BuildFilterClause(
        this LambdaExpression? lambda,
        Dictionary<string, object> bindVars)
    {
        if (lambda is null)
        {
            return string.Empty;
        }

        string where = lambda.BuildWhereClause(bindVars);
        return string.IsNullOrWhiteSpace(where) ? string.Empty : $" FILTER {where}";
    }

    public static string? BuildTopLevelMemberName(
        this LambdaExpression? lambda)
    {
        if (lambda is null)
        {
            return null;
        }

        Expression current = lambda.Body;
        string? topLevel = null;
        while (current is MemberExpression m)
        {
            // If the parent is the parameter (e.g., doc), this member is the top-level field
            if (m.Expression is ParameterExpression)
            {
                topLevel = m.Member.Name;
                break;
            }
            current = m.Expression!;
        }
        return topLevel;
    }

    public static string BuildWhereClause(
        this LambdaExpression lambda,
        Dictionary<string, object> bindVars)
    {
        return lambda.Body.BuildWhereClause(bindVars);
    }

    public static string BuildOrderByClause(
        this LambdaExpression lambda,
        Dictionary<string, object> bindVars)
    {
        return lambda.Body.BuildWhereClause(bindVars);
    }

    public static string BuildOperand(
        this LambdaExpression expr,
        Dictionary<string, object> bindVars)
    {
        object result = expr.Compile().DynamicInvoke();
        return result switch
        {
            string str => bindVars.AddBindVar(str),
            int number => bindVars.AddBindVar(number),
            float single => bindVars.AddBindVar(single),
            double dbl => bindVars.AddBindVar(dbl),
            decimal dec => bindVars.AddBindVar(dec),
            BigInteger bigInt => bindVars.AddBindVar(bigInt),
            List<string> strings => bindVars.AddBindVar(strings),
            List<int> ints => bindVars.AddBindVar(ints),
            List<float> floats => bindVars.AddBindVar(floats),
            List<double> doubles => bindVars.AddBindVar(doubles),
            List<decimal> decimals => bindVars.AddBindVar(decimals),
            List<BigInteger> bigInts => bindVars.AddBindVar(bigInts),
            IEnumerable<string> strings => bindVars.AddBindVar(strings.ToList()),
            IEnumerable<int> ints => bindVars.AddBindVar(ints.ToList()),
            IEnumerable<float> floats => bindVars.AddBindVar(floats.ToList()),
            IEnumerable<double> doubles => bindVars.AddBindVar(doubles.ToList()),
            IEnumerable<decimal> decimals => bindVars.AddBindVar(decimals.ToList()),
            IEnumerable<BigInteger> bigInts => bindVars.AddBindVar(bigInts.ToList()),
            _ => throw new NotSupportedException($"Unsupported expression: {expr.NodeType}")
        };

    }
}
