# IUserApiClient.DeleteUserAsync Method 
 

Delete a user permanently. You need Administrate for the server access level in order to execute this REST call.

**Namespace:**&nbsp;<a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<DeleteUserResponse> DeleteUserAsync(
	string username
)
```

**VB**<br />
``` VB
Function DeleteUserAsync ( 
	username As String
) As Task(Of DeleteUserResponse)
```

**C++**<br />
``` C++
Task<DeleteUserResponse^>^ DeleteUserAsync(
	String^ username
)
```

**F#**<br />
``` F#
abstract DeleteUserAsync : 
        username : string -> Task<DeleteUserResponse> 

```


#### Parameters
&nbsp;<dl><dt>username</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />The name of the user.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="07e6d9f1-2dcb-027f-655c-995184ddca92">DeleteUserResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.UserApi.IUserApiClient.DeleteUserAsync(System.String)"\]

## See Also


#### Reference
<a href="975b79fb-bac2-ed5a-a69e-98a986a268e2">IUserApiClient Interface</a><br /><a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi Namespace</a><br />