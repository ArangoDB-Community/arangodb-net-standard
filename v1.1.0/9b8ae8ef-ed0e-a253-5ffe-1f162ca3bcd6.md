# DatabaseApiClient.PostDatabaseAsync Method 
 

Creates a new database. (Only possible from within the _system database)

**Namespace:**&nbsp;<a href="8ff26d37-924f-7675-e479-50002d06bb9e">ArangoDBNetStandard.DatabaseApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<PostDatabaseResponse> PostDatabaseAsync(
	PostDatabaseBody request
)
```

**VB**<br />
``` VB
Public Overridable Function PostDatabaseAsync ( 
	request As PostDatabaseBody
) As Task(Of PostDatabaseResponse)
```

**C++**<br />
``` C++
public:
virtual Task<PostDatabaseResponse^>^ PostDatabaseAsync(
	PostDatabaseBody^ request
)
```

**F#**<br />
``` F#
abstract PostDatabaseAsync : 
        request : PostDatabaseBody -> Task<PostDatabaseResponse> 
override PostDatabaseAsync : 
        request : PostDatabaseBody -> Task<PostDatabaseResponse> 
```


#### Parameters
&nbsp;<dl><dt>request</dt><dd>Type: <a href="3577f4e0-e3e3-b704-431c-64eb28555607">ArangoDBNetStandard.DatabaseApi.Models.PostDatabaseBody</a><br />The parameters required by this endpoint.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="1ec87e21-a89d-4f77-a89d-bda2cd558562">PostDatabaseResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.DatabaseApi.DatabaseApiClient.PostDatabaseAsync(ArangoDBNetStandard.DatabaseApi.Models.PostDatabaseBody)"\]

#### Implements
<a href="1f9da93c-037d-1103-1c07-a8846f678aff">IDatabaseApiClient.PostDatabaseAsync(PostDatabaseBody)</a><br />

## See Also


#### Reference
<a href="5bc4e530-c688-14e5-3167-50be3b3b1173">DatabaseApiClient Class</a><br /><a href="8ff26d37-924f-7675-e479-50002d06bb9e">ArangoDBNetStandard.DatabaseApi Namespace</a><br />