# UserApiClient.DeleteUserAsync Method 
 

Delete a user permanently. You need Administrate for the server access level in order to execute this REST call.

**Namespace:**&nbsp;<a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public virtual Task<DeleteUserResponse> DeleteUserAsync(
	string username
)
```

**VB**<br />
``` VB
Public Overridable Function DeleteUserAsync ( 
	username As String
) As Task(Of DeleteUserResponse)
```

**C++**<br />
``` C++
public:
virtual Task<DeleteUserResponse^>^ DeleteUserAsync(
	String^ username
)
```

**F#**<br />
``` F#
abstract DeleteUserAsync : 
        username : string -> Task<DeleteUserResponse> 
override DeleteUserAsync : 
        username : string -> Task<DeleteUserResponse> 
```


#### Parameters
&nbsp;<dl><dt>username</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">System.String</a><br />The name of the user.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="07e6d9f1-2dcb-027f-655c-995184ddca92">DeleteUserResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.UserApi.UserApiClient.DeleteUserAsync(System.String)"\]

#### Implements
<a href="2ef1ffdc-bde8-8eef-bfa6-ebd72123ebdc">IUserApiClient.DeleteUserAsync(String)</a><br />

## See Also


#### Reference
<a href="f54e6b38-3de3-781d-5641-dfc7e1ee3ab4">UserApiClient Class</a><br /><a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi Namespace</a><br />