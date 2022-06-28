# IGraphApiClient.PutVertexAsync(*T*) Method (String, String, String, *T*, PutVertexQuery)
 

Replaces the data of a vertex in the collection. PUT/_api/gharial/{graph}/vertex/{collection}/{vertex}

**Namespace:**&nbsp;<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<PutVertexResponse<T>> PutVertexAsync<T>(
	string graphName,
	string collectionName,
	string key,
	T vertex,
	PutVertexQuery query = null
)

```

**VB**<br />
``` VB
Function PutVertexAsync(Of T) ( 
	graphName As String,
	collectionName As String,
	key As String,
	vertex As T,
	Optional query As PutVertexQuery = Nothing
) As Task(Of PutVertexResponse(Of T))
```

**C++**<br />
``` C++
generic<typename T>
Task<PutVertexResponse<T>^>^ PutVertexAsync(
	String^ graphName, 
	String^ collectionName, 
	String^ key, 
	T vertex, 
	PutVertexQuery^ query = nullptr
)
```

**F#**<br />
``` F#
abstract PutVertexAsync : 
        graphName : string * 
        collectionName : string * 
        key : string * 
        vertex : 'T * 
        ?query : PutVertexQuery 
(* Defaults:
        let _query = defaultArg query null
*)
-> Task<PutVertexResponse<'T>> 

```


#### Parameters
&nbsp;<dl><dt>graphName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />\[Missing <param name="graphName"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />\[Missing <param name="collectionName"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd><dt>key</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />\[Missing <param name="key"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd><dt>vertex</dt><dd>Type: *T*<br />\[Missing <param name="vertex"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd><dt>query (Optional)</dt><dd>Type: <a href="100ec490-6630-8e0d-0dc4-c72803422aeb">ArangoDBNetStandard.GraphApi.Models.PutVertexQuery</a><br />\[Missing <param name="query"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>\[Missing <typeparam name="T"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="1f9e4819-0cbc-7e04-a968-95d6e00ebad7">PutVertexResponse</a>(*T*))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutVertexAsync``1(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PutVertexQuery)"\]

## See Also


#### Reference
<a href="9cf68195-2611-f408-a78f-ab77864cc844">IGraphApiClient Interface</a><br /><a href="ac8dd67d-59c2-bbf4-e60a-0deeafa3b3e8">PutVertexAsync Overload</a><br /><a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi Namespace</a><br />