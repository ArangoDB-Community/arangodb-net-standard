# BulkOperationsApiClient.PostImportDocumentObjectsAsync Method (ImportDocumentsQuery, String)
 

Imports objects as documents into a collection. Use this method if you have already structured the JSON body according to the specifications. POST /_api/import

**Namespace:**&nbsp;<a href="58ea8fb7-f486-616b-9ed4-6982224f5f8d">ArangoDBNetStandard.BulkOperationsApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(
	ImportDocumentsQuery query,
	string jsonBody
)
```

**VB**<br />
``` VB
Public Overridable Function PostImportDocumentObjectsAsync ( 
	query As ImportDocumentsQuery,
	jsonBody As String
) As Task(Of ImportDocumentsResponse)
```

**C++**<br />
``` C++
public:
virtual Task<ImportDocumentsResponse^>^ PostImportDocumentObjectsAsync(
	ImportDocumentsQuery^ query, 
	String^ jsonBody
)
```

**F#**<br />
``` F#
abstract PostImportDocumentObjectsAsync : 
        query : ImportDocumentsQuery * 
        jsonBody : string -> Task<ImportDocumentsResponse> 
override PostImportDocumentObjectsAsync : 
        query : ImportDocumentsQuery * 
        jsonBody : string -> Task<ImportDocumentsResponse> 
```


#### Parameters
&nbsp;<dl><dt>query</dt><dd>Type: <a href="cccf0af5-eb4f-c35b-37c8-46f4a19d116e">ArangoDBNetStandard.BulkOperationsApi.Models.ImportDocumentsQuery</a><br />Options for the import.</dd><dt>jsonBody</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />The body of the request containing the required JSON objects.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="2cea7418-a2f2-1866-76be-d2009adce7ed">ImportDocumentsResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.BulkOperationsApi.BulkOperationsApiClient.PostImportDocumentObjectsAsync(ArangoDBNetStandard.BulkOperationsApi.Models.ImportDocumentsQuery,System.String)"\]

#### Implements
<a href="77288d10-8e60-e5df-3e96-231b9f3ba4a6">IBulkOperationsApiClient.PostImportDocumentObjectsAsync(ImportDocumentsQuery, String)</a><br />

## See Also


#### Reference
<a href="24c7579c-3368-eaf7-62c6-488b43f1ec43">BulkOperationsApiClient Class</a><br /><a href="700f817b-078a-9fdc-bd55-ede4c9e025cf">PostImportDocumentObjectsAsync Overload</a><br /><a href="58ea8fb7-f486-616b-9ed4-6982224f5f8d">ArangoDBNetStandard.BulkOperationsApi Namespace</a><br />