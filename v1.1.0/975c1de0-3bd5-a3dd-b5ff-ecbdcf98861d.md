# IGraphApiClient.PutEdgeDefinitionAsync Method 
 

Change one specific edge definition. This will modify all occurrences of this definition in all graphs known to your database. PUT/_api/gharial/{graph}/edge/{definition}

**Namespace:**&nbsp;<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<PutEdgeDefinitionResponse> PutEdgeDefinitionAsync(
	string graphName,
	string collectionName,
	PutEdgeDefinitionBody body,
	PutEdgeDefinitionQuery query = null
)
```

**VB**<br />
``` VB
Function PutEdgeDefinitionAsync ( 
	graphName As String,
	collectionName As String,
	body As PutEdgeDefinitionBody,
	Optional query As PutEdgeDefinitionQuery = Nothing
) As Task(Of PutEdgeDefinitionResponse)
```

**C++**<br />
``` C++
Task<PutEdgeDefinitionResponse^>^ PutEdgeDefinitionAsync(
	String^ graphName, 
	String^ collectionName, 
	PutEdgeDefinitionBody^ body, 
	PutEdgeDefinitionQuery^ query = nullptr
)
```

**F#**<br />
``` F#
abstract PutEdgeDefinitionAsync : 
        graphName : string * 
        collectionName : string * 
        body : PutEdgeDefinitionBody * 
        ?query : PutEdgeDefinitionQuery 
(* Defaults:
        let _query = defaultArg query null
*)
-> Task<PutEdgeDefinitionResponse> 

```


#### Parameters
&nbsp;<dl><dt>graphName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />\[Missing <param name="graphName"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutEdgeDefinitionAsync(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery)"\]</dd><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />\[Missing <param name="collectionName"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutEdgeDefinitionAsync(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery)"\]</dd><dt>body</dt><dd>Type: <a href="c82d7432-b6f9-05ed-3945-ba5c40735aa6">ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody</a><br />\[Missing <param name="body"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutEdgeDefinitionAsync(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery)"\]</dd><dt>query (Optional)</dt><dd>Type: <a href="adc579a8-b81a-a876-29ca-d2bda12cada7">ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery</a><br />\[Missing <param name="query"/> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutEdgeDefinitionAsync(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery)"\]</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="a794adc5-0b83-4e22-c564-ead8f242019a">PutEdgeDefinitionResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.GraphApi.IGraphApiClient.PutEdgeDefinitionAsync(System.String,System.String,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionBody,ArangoDBNetStandard.GraphApi.Models.PutEdgeDefinitionQuery)"\]

## See Also


#### Reference
<a href="9cf68195-2611-f408-a78f-ab77864cc844">IGraphApiClient Interface</a><br /><a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi Namespace</a><br />