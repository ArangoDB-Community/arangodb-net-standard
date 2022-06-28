# IGraphApiClient.PatchVertexAsync(*T*, *U*) Method (String, String, String, *T*, PatchVertexQuery)
 

Updates the data of the specific vertex in the collection. PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex}

**Namespace:**&nbsp;<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<PatchVertexResponse<U>> PatchVertexAsync<T, U>(
	string graphName,
	string collectionName,
	string vertexKey,
	T body,
	PatchVertexQuery query = null
)

```

**VB**<br />
``` VB
Function PatchVertexAsync(Of T, U) ( 
	graphName As String,
	collectionName As String,
	vertexKey As String,
	body As T,
	Optional query As PatchVertexQuery = Nothing
) As Task(Of PatchVertexResponse(Of U))
```

**C++**<br />
``` C++
generic<typename T, typename U>
Task<PatchVertexResponse<U>^>^ PatchVertexAsync(
	String^ graphName, 
	String^ collectionName, 
	String^ vertexKey, 
	T body, 
	PatchVertexQuery^ query = nullptr
)
```

**F#**<br />
``` F#
abstract PatchVertexAsync : 
        graphName : string * 
        collectionName : string * 
        vertexKey : string * 
        body : 'T * 
        ?query : PatchVertexQuery 
(* Defaults:
        let _query = defaultArg query null
*)
-> Task<PatchVertexResponse<'U>> 

```


#### Parameters
&nbsp;<dl><dt>graphName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>vertexKey</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>body</dt><dd>Type: *T*<br /></dd><dt>query (Optional)</dt><dd>Type: <a href="38756543-faa6-57a8-e43f-3631ab41ff07">ArangoDBNetStandard.GraphApi.Models.PatchVertexQuery</a><br /></dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Type of the patch object</dd><dt>U</dt><dd>Type of the returned document, only applies when <a href="2eaabc72-dcd1-8202-6711-4d5deec3c428">ReturnNew</a> or <a href="da6dae47-bce5-119e-ea42-507df1f8e722">ReturnOld</a> are used.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="acda1e7b-a4ea-02f4-624b-c84fffd587c8">PatchVertexResponse</a>(*U*))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PatchVertexAsync``2(System.String,System.String,System.String,``0,ArangoDBNetStandard.GraphApi.Models.PatchVertexQuery)"\]

## See Also


#### Reference
<a href="9cf68195-2611-f408-a78f-ab77864cc844">IGraphApiClient Interface</a><br /><a href="d7124eea-3886-08fc-cbd4-f495a8343817">PatchVertexAsync Overload</a><br /><a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi Namespace</a><br />