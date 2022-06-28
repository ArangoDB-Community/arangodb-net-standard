# IUserApiClient.GetUsersAsync Method 
 

Fetches data about all users. You need the Administrate server access level in order to execute this REST call. Otherwise, you will only get information about yourself.

**Namespace:**&nbsp;<a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
Task<GetUsersResponse> GetUsersAsync()
```

**VB**<br />
``` VB
Function GetUsersAsync As Task(Of GetUsersResponse)
```

**C++**<br />
``` C++
Task<GetUsersResponse^>^ GetUsersAsync()
```

**F#**<br />
``` F#
abstract GetUsersAsync : unit -> Task<GetUsersResponse> 

```


#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1" target="_blank" rel="noopener noreferrer">Task</a>(<a href="85142da0-494e-ddd5-7b5d-049ca01f8254">GetUsersResponse</a>)<br />\[Missing <returns> documentation for "M:ArangoDBNetStandard.UserApi.IUserApiClient.GetUsersAsync"\]

## See Also


#### Reference
<a href="975b79fb-bac2-ed5a-a69e-98a986a268e2">IUserApiClient Interface</a><br /><a href="a57cb14e-62d0-8e40-f4e2-560f8a8cd6e9">ArangoDBNetStandard.UserApi Namespace</a><br />