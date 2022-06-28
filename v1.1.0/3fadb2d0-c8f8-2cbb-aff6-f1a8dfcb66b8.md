# DocumentApiClient.PutDocumentsAsync(*T*) Method 
 

Replace multiple documents.

**Namespace:**&nbsp;<a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<PutDocumentsResponse<T>> PutDocumentsAsync<T>(
	string collectionName,
	IList<T> documents,
	PutDocumentsQuery query = null,
	ApiClientSerializationOptions serializationOptions = null,
	DocumentHeaderProperties headers = null
)

```

**VB**<br />
``` VB
Public Overridable Function PutDocumentsAsync(Of T) ( 
	collectionName As String,
	documents As IList(Of T),
	Optional query As PutDocumentsQuery = Nothing,
	Optional serializationOptions As ApiClientSerializationOptions = Nothing,
	Optional headers As DocumentHeaderProperties = Nothing
) As Task(Of PutDocumentsResponse(Of T))
```

**C++**<br />
``` C++
public:
generic<typename T>
virtual Task<PutDocumentsResponse<T>^>^ PutDocumentsAsync(
	String^ collectionName, 
	IList<T>^ documents, 
	PutDocumentsQuery^ query = nullptr, 
	ApiClientSerializationOptions^ serializationOptions = nullptr, 
	DocumentHeaderProperties^ headers = nullptr
)
```

**F#**<br />
``` F#
abstract PutDocumentsAsync : 
        collectionName : string * 
        documents : IList<'T> * 
        ?query : PutDocumentsQuery * 
        ?serializationOptions : ApiClientSerializationOptions * 
        ?headers : DocumentHeaderProperties 
(* Defaults:
        let _query = defaultArg query null
        let _serializationOptions = defaultArg serializationOptions null
        let _headers = defaultArg headers null
*)
-> Task<PutDocumentsResponse<'T>> 
override PutDocumentsAsync : 
        collectionName : string * 
        documents : IList<'T> * 
        ?query : PutDocumentsQuery * 
        ?serializationOptions : ApiClientSerializationOptions * 
        ?headers : DocumentHeaderProperties 
(* Defaults:
        let _query = defaultArg query null
        let _serializationOptions = defaultArg serializationOptions null
        let _headers = defaultArg headers null
*)
-> Task<PutDocumentsResponse<'T>> 
```


#### Parameters
&nbsp;<dl><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>documents</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.collections.generic.ilist-1" target="_blank" rel="noopener noreferrer">System.Collections.Generic.IList</a>(*T*)<br /></dd><dt>query (Optional)</dt><dd>Type: <a href="6b7d9660-6073-52cb-ee0c-b95bdf513138">ArangoDBNetStandard.DocumentApi.Models.PutDocumentsQuery</a><br /></dd><dt>serializationOptions (Optional)</dt><dd>Type: <a href="4d2cfe44-8a3a-2efb-e814-c882bbee3e85">ArangoDBNetStandard.Serialization.ApiClientSerializationOptions</a><br />The serialization options. When the value is null the the serialization options should be provided by the serializer, otherwise the given options should be used.</dd><dt>headers (Optional)</dt><dd>Type: <a href="ec926014-3226-807e-03cf-3e590a993eb8">ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties</a><br />The <a href="ec926014-3226-807e-03cf-3e590a993eb8">DocumentHeaderProperties</a> values.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd /></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="2d7607d1-805e-7921-0e20-150a9adbe96d">PutDocumentsResponse</a>(*T*))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.DocumentApi.DocumentApiClient.PutDocumentsAsync``1(System.String,System.Collections.Generic.IList{``0},ArangoDBNetStandard.DocumentApi.Models.PutDocumentsQuery,ArangoDBNetStandard.Serialization.ApiClientSerializationOptions,ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties)"\]

#### Implements
<a href="2b0a56c1-ebad-5941-16df-a2228ed83295">IDocumentApiClient.PutDocumentsAsync(T)(String, IList(T), PutDocumentsQuery, ApiClientSerializationOptions, DocumentHeaderProperties)</a><br />

## See Also


#### Reference
<a href="cd42246b-93a7-65bc-606d-b54b1f465670">DocumentApiClient Class</a><br /><a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi Namespace</a><br />