# AqlFunctionApiClient.PostExplainAqlQueryAsync Method 
 

Explain an AQL query and return information about it POST /_api/explain

**Namespace:**&nbsp;<a href="9e7a61c2-48d3-6f6b-39e9-eee0bd305b09">ArangoDBNetStandard.AqlFunctionApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<PostExplainAqlQueryResponse> PostExplainAqlQueryAsync(
	PostExplainAqlQueryBody body
)
```

**VB**<br />
``` VB
Public Overridable Function PostExplainAqlQueryAsync ( 
	body As PostExplainAqlQueryBody
) As Task(Of PostExplainAqlQueryResponse)
```

**C++**<br />
``` C++
public:
virtual Task<PostExplainAqlQueryResponse^>^ PostExplainAqlQueryAsync(
	PostExplainAqlQueryBody^ body
)
```

**F#**<br />
``` F#
abstract PostExplainAqlQueryAsync : 
        body : PostExplainAqlQueryBody -> Task<PostExplainAqlQueryResponse> 
override PostExplainAqlQueryAsync : 
        body : PostExplainAqlQueryBody -> Task<PostExplainAqlQueryResponse> 
```


#### Parameters
&nbsp;<dl><dt>body</dt><dd>Type: <a href="d0f316e8-bde3-89ce-064c-a7cb54b4d11c">ArangoDBNetStandard.AqlFunctionApi.Models.PostExplainAqlQueryBody</a><br />The body of the request containing required properties.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="050a5d07-8a5c-112e-1d16-c6b87958553f">PostExplainAqlQueryResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.AqlFunctionApi.AqlFunctionApiClient.PostExplainAqlQueryAsync(ArangoDBNetStandard.AqlFunctionApi.Models.PostExplainAqlQueryBody)"\]

#### Implements
<a href="830efda5-14a3-b97a-0041-828ab8598011">IAqlFunctionApiClient.PostExplainAqlQueryAsync(PostExplainAqlQueryBody)</a><br />

## See Also


#### Reference
<a href="93a70d3e-43eb-c1f0-6613-b8427d240577">AqlFunctionApiClient Class</a><br /><a href="9e7a61c2-48d3-6f6b-39e9-eee0bd305b09">ArangoDBNetStandard.AqlFunctionApi Namespace</a><br />