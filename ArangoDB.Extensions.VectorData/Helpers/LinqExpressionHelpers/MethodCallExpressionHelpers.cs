using System.Linq.Expressions;
using System.Numerics;

namespace ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

internal static class MethodCallExpressionHelpers
{
    /// <summary>
    /// Handles Method Calls in the <see cref="Expression{Delegate}"/> lambda expression in order to generate filter condition in the AQL.
    /// <i>here <see cref="Delegate"/> is <seealso cref="Func{T, TResult}"/> where T is any type and TResult is <see cref="bool"/></i>.
    /// It can handle the following methods:
    /// <list type="table">
    ///     <item>
    ///         <h1>Equals Operator</h1>
    ///         <code>
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name == "test";
    ///             <strong>Produces: </strong>FILTER doc.Name == @param1
    ///         </code> 
    ///     </item>
    ///     <item>
    ///         <h1><see cref="string.Equals(string)"/> Method</h1>
    ///         <code>
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Equals("test");
    ///             <strong>Produces: </strong>FILTER doc.Name == @param1
    ///         </code> 
    ///     </item>
    ///     <item>
    ///         <h1>
    ///             <see cref="AqlFilters.Like(string, string)"/>, <see cref="AqlFilters.Like(string, string, StringComparison)"/>, <see cref="AqlFilters.Like(string, string, AqlLikeWildcardPositions)"/> and <see cref="AqlFilters.Like(string, string, StringComparison, AqlLikeWildcardPositions)"/> Extension Methods
    ///         </h1>
    ///         <code>
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Like("test", <see cref="StringComparison.OrdinalIgnoreCase"/>, <see cref="AqlLikeWildcardPositions.Both"/>);
    ///             <strong>Produces: </strong>FILTER LIKE (doc.Name, @param1). (When <see cref="StringComparison"/> is not provided or <see cref="StringComparison.Ordinal"/>, <see cref="StringComparison.CurrentCulture"/>, <see cref="StringComparison.InvariantCulture"/>)
    ///             <strong>Produces: </strong>FILTER LIKE (doc.Name, @param1, true). (When <see cref="StringComparison"/> is provided as <see cref="StringComparison.OrdinalIgnoreCase"/>, <see cref="StringComparison.InvariantCultureIgnoreCase"/>, <see cref="StringComparison.InvariantCultureIgnoreCase"/>)
    ///             <i><strong>param1</strong> would be enclose with wildcards at the both side like <strong>%abc%</strong></i> if <see cref="AqlLikeWildcardPositions.Both"/> is used or not used at all. 
    ///             Otherwise it will use <strong>%abc</strong> for <see cref="AqlLikeWildcardPositions.Start"/> or <strong>abc%</strong> for <see cref="AqlLikeWildcardPositions.End"/>
    ///         </code>
    ///     </item>
    ///     <item>
    ///         <h1>
    ///             <see cref="string.Contains(string)"/> Method
    ///             <br/>
    ///             <see cref="string.Contains(string, StringComparison)"/> Method
    ///         </h1>
    ///         <code>
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Contains("test");
    ///             <strong>Produces: </strong>FILTER CONTAINS(doc.Name, @param1)
    ///         </code>
    ///         <code>
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Contains("test", <see cref="StringComparison.OrdinalIgnoreCase"/>);
    ///             <strong>Produces: </strong>FILTER CONTAINS(LOWER(doc.Name), LOWER(@param1))
    ///         </code>
    ///         <code>
    ///             <see cref="string"/> filterText = "test";
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Contains(filterText);
    ///             <strong>Produces: </strong>FILTER CONTAINS(doc.Name, @param1)
    ///         </code>
    ///         <code>
    ///             <see cref="string"/> filterText = "test";
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Contains(filterText, <see cref="StringComparison.OrdinalIgnoreCase"/>);
    ///             <strong>Produces: </strong>FILTER CONTAINS(LOWER(doc.Name), LOWER(@param1))
    ///         </code>
    ///     </item>
    ///     <item>
    ///         <h1><see cref="List{string}.Contains(string)"/> Method</h1>
    ///         <code>
    ///             <see cref="List{string}"/> filterTexts = ["test"];
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; filterTexts.Contains(r.Name);
    ///             <strong>Produces: </strong>FILTER doc.Name in @param1
    ///             <i><strong>Example: </strong>FILTER doc.Name in ["test"]</i>
    ///             <br/>
    ///             <i><strong>Note: </strong><see cref="List{T}"/> supports <see cref="string"/>, <see cref="int"/>, <see cref="float"/>, <see cref="double"/>, <see cref="decimal"/> and <see cref="BigInteger"/>.</i>
    ///         </code>
    ///     </item>
    ///     <item>
    ///         <h1><see cref="Enumerable.Contains{string}(IEnumerable{string}, string)"/> Extension Method</h1>
    ///         <code>
    ///             <see cref="IEnumerable{string}"/> filterTexts = ["test"];
    ///             Expression&lt;Func&lt;TestRecord, bool&gt;&gt; filter = r =&gt; r.Name.Contains(filterText, <see cref="StringComparison.OrdinalIgnoreCase"/>);
    ///             <strong>Produces: </strong>FILTER doc.Name in @param1
    ///             <i><strong>Example: </strong>FILTER doc.Name in ["test"]</i>
    ///             <br/>
    ///             <i><strong>Note: </strong><see cref="IEnumerable{T}"/> supports <see cref="string"/>, <see cref="int"/>, <see cref="float"/>, <see cref="double"/>, <see cref="decimal"/> and <see cref="BigInteger"/>.</i>
    ///         </code>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="mce"></param>
    /// <param name="bindVars"></param>
    /// <returns></returns>
    public static string HandleOperationInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars)
    {
        return mce switch
        {
            // r => r.Name.Like("test") Extension Method
            { Method.Name: nameof(AqlFilters.Like), Arguments.Count: 2 }
                => mce.HandleLikeOperationInFilterCondition(bindVars),

            // IEnumerable<string> list = [ "test" ];
            // r => list.Contains(r.Name) Extension Method
            { Method.Name: nameof(Enumerable.Contains) }
               when mce is { Arguments.Count: 2 }
                 && mce.Arguments[0] is MemberExpression
                 && mce.Arguments[1] is MemberExpression
                 && mce.Method.IsStatic
               => mce.HandleCollectionContainsOperationInFilterCondition(bindVars),

            // List<string> list = [ "test" ];
            // r => list.Contains(r.Name) Instance Method
            { Method.Name: nameof(List<>.Contains) }
                when mce is { Object: MemberExpression, Arguments.Count: 1 }
                  && mce.Arguments[0] is MemberExpression
                  && (mce.Method.DeclaringType == typeof(List<string>))
                => mce.HandleListContainsOperationInFilterCondition(bindVars),

            // string filterText = "test";
            // r => r.Name.Contains(filterText) Instance Method and when param is a lambda expression
            { Method.Name: nameof(string.Contains) }
                when mce is { Object: MemberExpression, Arguments.Count: 1 }
                  && mce.Arguments[0] is MemberExpression me
                  && Expression.Lambda(me) is LambdaExpression lambdaParam
                => mce.HandleStringContainsOperationInFilterCondition(bindVars, lambdaParam),

            // r => r.Name.Contains("test") Instance Method and when param is a constant expression 
            { Method.Name: nameof(string.Contains) }
                when mce is { Object: MemberExpression, Arguments.Count: 1 }
                  && mce.Arguments[0] is ConstantExpression
                => mce.HandleStringContainsOperationInFilterCondition(bindVars),

            // string filterText = "test";
            // r => r.Name.Equals(filterText) Instance Method and when param is a lambda expression
            { Method.Name: nameof(string.Contains) }
                when mce is { Object: MemberExpression, Arguments.Count: 1 }
                  && mce.Arguments[0] is MemberExpression me
                  && Expression.Lambda(me) is LambdaExpression lambdaParam
                => mce.HandleEqualsOperationsInFilterCondition(bindVars, lambdaParam),

            // r => r.Name.Equals("test") Instance Method and when param is a constant expression
            { Method.Name: nameof(string.Equals) }
                => mce.HandleEqualsOperationsInFilterCondition(bindVars),
            _ => string.Empty
        };
    }

    public static string HandleLikeOperationInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars)
    {
        return mce switch
        {
            // r => r.Name.Like("test")
            { Arguments.Count: 2 }
                  when mce.Arguments[0] is MemberExpression memberExpr
                  && mce.Arguments[1] is ConstantExpression valueExpr
                => $"LIKE ({memberExpr.BuildMemberAccess()}, {valueExpr.BuildOperand(bindVars, mce)})",
            // r => r.Name.Like("test", StringComparison.OrdinalIgnoreCase)
            { Arguments.Count: 3 }
                  when mce.Arguments[0] is MemberExpression memberExpr
                  && mce.Arguments[1] is ConstantExpression valueExpr
                  && mce.Arguments[2] is ConstantExpression comparisonExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                => $"LIKE ({memberExpr.BuildMemberAccess()}, {valueExpr.BuildOperand(bindVars, mce)}, {stringComparison.GetComparisonOptionsForLike()})",
            // r => r.Name.Like("test", AqlLikeWildcardPositions.Both)
            { Arguments.Count: 3 }
                  when mce.Arguments[0] is MemberExpression memberExpr
                  && mce.Arguments[1] is ConstantExpression valueExpr
                  && mce.Arguments[2] is ConstantExpression wildcardExpression
                  && wildcardExpression.Value is AqlLikeWildcardPositions wildcardPosition
                => $"LIKE ({memberExpr.BuildMemberAccess()}, {valueExpr.BuildOperand(bindVars, mce, wildcardPosition)}, true)",
            // r => r.Name.Like("test", StringComparison.OrdinalIgnoreCase, AqlLikeWildcardPositions.Both)
            { Arguments.Count: 4 }
                  when mce.Arguments[0] is MemberExpression memberExpr
                  && mce.Arguments[1] is ConstantExpression valueExpr
                  && mce.Arguments[2] is ConstantExpression comparisonExpression
                  && mce.Arguments[3] is ConstantExpression wildcardExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                  && wildcardExpression.Value is AqlLikeWildcardPositions wildcardPosition
                => $"LIKE ({memberExpr.BuildMemberAccess()}, {valueExpr.BuildOperand(bindVars, mce, wildcardPosition)}, {stringComparison.GetComparisonOptionsForLike()})",
            _ => throw new NotSupportedException($"Unsupported expression: {mce.NodeType}")
        };
    }

    public static string HandleStringContainsOperationInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars,
        LambdaExpression? lambdaParam = null)
    {
        return mce switch
        {
            // r => r.Name.Contains("test")
            { Object: MemberExpression memberExpr, Arguments.Count: 1 }
                when mce.Arguments[0] is ConstantExpression ce
                => $"CONTAINS ({memberExpr.BuildMemberAccess()}, {ce.BuildOperand(bindVars)})",

            // r => r.Name.Contains("test")
            { Object: MemberExpression memberExpr, Arguments.Count: 2 }
                when mce.Arguments[0] is ConstantExpression constantExpression
                  && mce.Arguments[1] is ConstantExpression comparisonExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                  && string.IsNullOrWhiteSpace(stringComparison.GetComparisonOptionsForContainsOrEquals())
                => $"CONTAINS ({memberExpr.BuildMemberAccess()}, {constantExpression.BuildOperand(bindVars)})",

            // r => r.Name.Contains("test", STringComparison.OrdinalIgnoreCase)
            { Object: MemberExpression memberExpr, Arguments.Count: 2 }
                when mce.Arguments[0] is ConstantExpression constantExpression
                  && mce.Arguments[1] is ConstantExpression comparisonExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                  && stringComparison.GetComparisonOptionsForContainsOrEquals() is string stringComparisonOp
                => $"CONTAINS ({stringComparisonOp}({memberExpr.BuildMemberAccess()}), {stringComparisonOp}({constantExpression.BuildOperand(bindVars)}))",

            // string filterText = "test";
            // r => r.Name.Contains(filterText)
            { Object: MemberExpression memberExpr, Arguments.Count: 1 }
                when mce.Arguments[0] is MemberExpression
                  && lambdaParam is not null
                => $"CONTAINS ({memberExpr.BuildMemberAccess()}, {lambdaParam.BuildOperand(bindVars)})",
            _ => throw new NotSupportedException($"Unsupported expression: {mce.NodeType}")
        };
    }

    public static string HandleListContainsOperationInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars)
    {
        return mce switch
        {
            // List<string> list = [ "test" ];
            // r => list.Contains(r.Name)
            { Object: MemberExpression collectionExpr, Arguments.Count: 1 }
                when mce.Arguments[0] is MemberExpression memberExpr
                  && Expression.Lambda(collectionExpr) is var lambda
                  && lambda is not null
                => $"{memberExpr.BuildMemberAccess()} in {lambda.BuildOperand(bindVars)}",
            _ => throw new NotSupportedException($"Unsupported expression: {mce.NodeType}")
        };
    }

    public static string HandleCollectionContainsOperationInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars)
    {
        return mce switch
        {
            // IEnumerable<string> list = [ "test" ];
            // r => list.Contains(r.Name)
            { Arguments.Count: 2 }
                when mce.Arguments[0] is MemberExpression collectionExpr
                  && Expression.Lambda(collectionExpr) is var lambda
                  && lambda is not null
                  && mce.Arguments[1] is MemberExpression memberExpr
                => $"{memberExpr.BuildMemberAccess()} in {lambda.BuildOperand(bindVars)}",
            _ => throw new NotSupportedException($"Unsupported expression: {mce.NodeType}")
        };
    }

    public static string HandleEqualsOperationsInFilterCondition(
        this MethodCallExpression mce,
        Dictionary<string, object> bindVars,
        LambdaExpression? lambdaParam = null)
    {
        return mce switch
        {
            // r => r.Name.Equals("test")
            { Object: MemberExpression memberExpr, Arguments.Count: 1 }
                when mce.Arguments[0] is ConstantExpression constantExpression
                => $"{memberExpr.BuildMemberAccess()} == {constantExpression.BuildOperand(bindVars)}",
            
            // r => r.Name.Equals("test")
            { Object: MemberExpression memberExpr, Arguments.Count: 2 }
                when mce.Arguments[0] is ConstantExpression constantExpression
                  && mce.Arguments[1] is ConstantExpression comparisonExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                  && string.IsNullOrWhiteSpace(stringComparison.GetComparisonOptionsForContainsOrEquals())
                => $"{memberExpr.BuildMemberAccess()} == {constantExpression.BuildOperand(bindVars)}",

            // r => r.Name.Equals("test", StringComparison.OrdinalIgnoreCase)
            { Object: MemberExpression memberExpr, Arguments.Count: 2 }
            when mce.Arguments[0] is ConstantExpression constantExpression
                  && mce.Arguments[1] is ConstantExpression comparisonExpression
                  && comparisonExpression.Value is StringComparison stringComparison
                  && stringComparison.GetComparisonOptionsForContainsOrEquals() is string stringComparisonOp
                => $"{stringComparisonOp}({memberExpr.BuildMemberAccess()}) == {stringComparisonOp}({constantExpression.BuildOperand(bindVars)})",

            // string filterText = "test";
            // r => r.Name.Equals(filterText)
            { Object: MemberExpression memberExpr, Arguments.Count: 1 }
                when mce.Arguments[0] is MemberExpression
                  && lambdaParam is not null
                => $"{memberExpr.BuildMemberAccess()} == {lambdaParam.BuildOperand(bindVars)}",

            _ => throw new NotSupportedException($"Unsupported expression: {mce.NodeType}")
        };
    }
}
