# CollectionApiClient.GetCollectionPropertiesAsync Method 
 

Read properties of a collection. GET /_api/collection/{collection-name}/properties

**Namespace:**&nbsp;<a href="3dcc286c-06c5-3dac-bfbd-fb449b69cd48">ArangoDBNetStandard.CollectionApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(
	string collectionName
)
```

**VB**<br />
``` VB
Public Overridable Function GetCollectionPropertiesAsync ( 
	collectionName As String
) As Task(Of GetCollectionPropertiesResponse)
```

**C++**<br />
``` C++
public:
virtual Task<GetCollectionPropertiesResponse^>^ GetCollectionPropertiesAsync(
	String^ collectionName
)
```

**F#**<br />
``` F#
abstract GetCollectionPropertiesAsync : 
        collectionName : string -> Task<GetCollectionPropertiesResponse> 
override GetCollectionPropertiesAsync : 
        collectionName : string -> Task<GetCollectionPropertiesResponse> 
```


#### Parameters
&nbsp;<dl><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="e10e7b86-a831-f90c-c2d1-6c0b2f89dbab">GetCollectionPropertiesResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.CollectionApi.CollectionApiClient.GetCollectionPropertiesAsync(System.String)"\]

#### Implements
<a href="316cc912-f746-644c-f81e-b5ed5c6b0959">ICollectionApiClient.GetCollectionPropertiesAsync(String)</a><br />

## See Also


#### Reference
<a href="6ce48613-2e1c-4702-c589-43e91c706f90">CollectionApiClient Class</a><br /><a href="3dcc286c-06c5-3dac-bfbd-fb449b69cd48">ArangoDBNetStandard.CollectionApi Namespace</a><br />