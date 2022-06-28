# DocumentApiClient.DeleteDocumentsAsync Method (String, IList(String), DeleteDocumentsQuery, DocumentHeaderProperties)
 

Delete multiple documents based on the passed document selectors. A document selector is either the document ID or the document Key.

**Namespace:**&nbsp;<a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<DeleteDocumentsResponse<Object>> DeleteDocumentsAsync(
	string collectionName,
	IList<string> selectors,
	DeleteDocumentsQuery query = null,
	DocumentHeaderProperties headers = null
)
```

**VB**<br />
``` VB
Public Overridable Function DeleteDocumentsAsync ( 
	collectionName As String,
	selectors As IList(Of String),
	Optional query As DeleteDocumentsQuery = Nothing,
	Optional headers As DocumentHeaderProperties = Nothing
) As Task(Of DeleteDocumentsResponse(Of Object))
```

**C++**<br />
``` C++
public:
virtual Task<DeleteDocumentsResponse<Object^>^>^ DeleteDocumentsAsync(
	String^ collectionName, 
	IList<String^>^ selectors, 
	DeleteDocumentsQuery^ query = nullptr, 
	DocumentHeaderProperties^ headers = nullptr
)
```

**F#**<br />
``` F#
abstract DeleteDocumentsAsync : 
        collectionName : string * 
        selectors : IList<string> * 
        ?query : DeleteDocumentsQuery * 
        ?headers : DocumentHeaderProperties 
(* Defaults:
        let _query = defaultArg query null
        let _headers = defaultArg headers null
*)
-> Task<DeleteDocumentsResponse<Object>> 
override DeleteDocumentsAsync : 
        collectionName : string * 
        selectors : IList<string> * 
        ?query : DeleteDocumentsQuery * 
        ?headers : DocumentHeaderProperties 
(* Defaults:
        let _query = defaultArg query null
        let _headers = defaultArg headers null
*)
-> Task<DeleteDocumentsResponse<Object>> 
```


#### Parameters
&nbsp;<dl><dt>collectionName</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br /></dd><dt>selectors</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.collections.generic.ilist-1" target="_blank" rel="noopener noreferrer">System.Collections.Generic.IList</a>(<a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>)<br /></dd><dt>query (Optional)</dt><dd>Type: <a href="d4dc5177-3a85-3bf8-b1c3-cc9c23b7a233">ArangoDBNetStandard.DocumentApi.Models.DeleteDocumentsQuery</a><br /></dd><dt>headers (Optional)</dt><dd>Type: <a href="ec926014-3226-807e-03cf-3e590a993eb8">ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties</a><br />The <a href="ec926014-3226-807e-03cf-3e590a993eb8">DocumentHeaderProperties</a> values.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="1d4c2279-2070-815f-255d-176082e4d58e">DeleteDocumentsResponse</a>(<a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>))<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.DocumentApi.DocumentApiClient.DeleteDocumentsAsync(System.String,System.Collections.Generic.IList{System.String},ArangoDBNetStandard.DocumentApi.Models.DeleteDocumentsQuery,ArangoDBNetStandard.DocumentApi.Models.DocumentHeaderProperties)"\]

#### Implements
<a href="7752b8a7-021b-9f3a-3bf9-a005e462f664">IDocumentApiClient.DeleteDocumentsAsync(String, IList(String), DeleteDocumentsQuery, DocumentHeaderProperties)</a><br />

## Remarks
This method overload is provided as a convenience when the client does not care about the type of <a href="579a4b8c-59f2-2f2b-5c35-5c884e098099">Old</a> in the returned <a href="1d4c2279-2070-815f-255d-176082e4d58e">DeleteDocumentsResponse(T)</a>. These will be `null` when <a href="dd45cb73-b688-2cab-b9f3-7ebba9a513d1">ReturnOld</a> is either `false` or not set, so this overload is useful in the default case when deleting documents.

## See Also


#### Reference
<a href="cd42246b-93a7-65bc-606d-b54b1f465670">DocumentApiClient Class</a><br /><a href="a96eafd6-90bd-7341-fc6e-86fd86a16bf5">DeleteDocumentsAsync Overload</a><br /><a href="927cb31f-380a-2bf4-a1ca-09ab720e232b">ArangoDBNetStandard.DocumentApi Namespace</a><br />