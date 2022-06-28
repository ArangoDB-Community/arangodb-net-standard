# GraphApiClient.GetVertexAsync(*T*) Method (String, String, GetVertexQuery)
 

Gets a vertex based on its document ID.

**Namespace:**&nbsp;<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<GetVertexResponse<T>> GetVertexAsync<T>(
	string graphName,
	string documentId,
	GetVertexQuery query = null
)

```

**VB**<br />
``` VB
Public Overridable Function GetVertexAsync(Of T) ( 
	graphName As String,
	documentId As String,
	Optional query As GetVertexQuery = Nothing
) As Task(Of GetVertexResponse(Of T))
```

**C++**<br />
``` C++
public:
generic<typename T>
virtual Task<GetVertexResponse<T>^>^ GetVertexAsync(
	String^ graphName, 
	String^ documentId, 
	GetVertexQuery^ query = nullptr
)
```

**F#**<br />
``` F#
abstract GetVertexAsync : 
        graphName : string * 
        documentId : string * 
        ?query : GetVertexQuery 
(* Defaults:
        let _query = defaultArg query null
*)
-> Task<GetVertexResponse<'T>> 
override GetVertexAsync : 
        graphName : string * 
        documentId : string * 
        ?query : GetVertexQuery 
(* Defaults:
        let _query = defaultArg query null
*)
-> Task<GetVertexResponse<'T>> 
```


#### Parameters
&nbsp;<dl><dt>graphName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />The name of the graph to get the vertex from.</dd><dt>documentId</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />The document ID of the vertex to retrieve.</dd><dt>query (Optional)</dt><dd>Type: <a href="5f2e56ce-e226-1df5-9ff4-8b0199866956">ArangoDBNetStandard.GraphApi.Models.GetVertexQuery</a><br /></dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>\[Missing <typeparam name="T"/> documentation for "M:ArangoDBNetStandard.GraphApi.GraphApiClient.GetVertexAsync``1(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.GetVertexQuery)"\]</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="ee040d9e-e0a4-5046-783b-11f895185fd5">GetVertexResponse</a>(*T*))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.GraphApi.GraphApiClient.GetVertexAsync``1(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.GetVertexQuery)"\]

#### Implements
<a href="b1b20bad-586f-91e6-af6c-47bac0784608">IGraphApiClient.GetVertexAsync(T)(String, String, GetVertexQuery)</a><br />

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentexception" target="_blank" rel="noopener noreferrer">ArgumentException</a></td><td>Provided document ID is invalid.</td></tr></table>

## See Also


#### Reference
<a href="fbeb06c2-7ca5-a17a-b0c2-96abac64dfaa">GraphApiClient Class</a><br /><a href="1447fbb8-8f87-b752-0531-b89d0ac87c2d">GetVertexAsync Overload</a><br /><a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi Namespace</a><br />