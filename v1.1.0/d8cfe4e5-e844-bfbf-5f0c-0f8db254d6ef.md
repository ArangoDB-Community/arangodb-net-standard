# IDocumentApiClient.PostDocumentsAsync(*T*) Method 
 

Post multiple documents in a single request.

**Namespace:**&nbsp;<a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<PostDocumentsResponse<T>> PostDocumentsAsync<T>(
	string collectionName,
	IList<T> documents,
	PostDocumentsQuery query = null,
	ApiClientSerializationOptions serializationOptions = null,
	DocumentHeaderProperties headers = null
)

```

**VB**<br />
``` VB
Function PostDocumentsAsync(Of T) ( 
	collectionName As String,
	documents As IList(Of T),
	Optional query As PostDocumentsQuery = Nothing,
	Optional serializationOptions As ApiClientSerializationOptions = Nothing,
	Optional headers As DocumentHeaderProperties = Nothing
) As Task(Of PostDocumentsResponse(Of T))
```

**C++**<br />
``` C++
generic<typename T>
Task<PostDocumentsResponse<T>^>^ PostDocumentsAsync(
	String^ collectionName, 
	IList<T>^ documents, 
	PostDocumentsQuery^ query = nullptr, 
	ApiClientSerializationOptions^ serializationOptions = nullptr, 
	DocumentHeaderProperties^ headers = nullptr
)
```

**F#**<br />
``` F#
abstract PostDocumentsAsync : 
        collectionName : string * 
        documents : IList<'T> * 
        ?query : PostDocumentsQuery * 
        ?serializationOptions : ApiClientSerializationOptions * 
        ?headers : DocumentHeaderProperties 
(* Defaults:
        let _query = defaultArg query null
        let _serializationOptions = defaultArg serializationOptions null
        let _headers = defaultArg headers null
*)
-> Task<PostDocumentsResponse<'T>> 

```


#### Parameters
&nbsp;<dl><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>documents</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.collections.generic.ilist-1" target="_blank" rel="noopener noreferrer">System.Collections.Generic.IList</a>(*T*)<br /></dd><dt>query (Optional)</dt><dd>Type: <a href="88665237-5f7b-22eb-07de-d6d70936ce1d">ArangoDBNetStandard.DocumentApi.Models.PostDocumentsQuery</a><br /></dd><dt>serializationOptions (Optional)</dt><dd>Type: <a href="4d2cfe44-8a3a-2efb-e814-c882bbee3e85">ArangoDBNetStandard.Serialization.ApiClientSerializationOptions</a><br />The serialization options. When the value is null the the serialization options should be provided by the serializer, otherwise the given options should be used.</dd><dt>headers (Optional)</dt><dd>Type: <a href="ec926014-3226-807e-03cf-3e590a993eb8">ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties</a><br />The <a href="ec926014-3226-807e-03cf-3e590a993eb8">DocumentHeaderProperties</a> values.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd /></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="03ca7550-0479-726e-4822-56898c2e1581">PostDocumentsResponse</a>(*T*))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.DocumentApi.IDocumentApiClient.PostDocumentsAsync``1(System.String,System.Collections.Generic.IList{``0},ArangoDBNetStandard.DocumentApi.Models.PostDocumentsQuery,ArangoDBNetStandard.Serialization.ApiClientSerializationOptions,ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties)"\]

## See Also


#### Reference
<a href="51df5b95-04af-da7c-e481-e78cd0e61d1c">IDocumentApiClient Interface</a><br /><a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi Namespace</a><br />