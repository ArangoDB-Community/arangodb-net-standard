# PostCollectionQuery.WaitForSyncReplication Property 
 

Default is true which means the server will only report success back to the client if all replicas have created the collection. Set to false if you want faster server responses and don’t care about full replication.

**Namespace:**&nbsp;<a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? WaitForSyncReplication { get; set; }
```

**VB**<br />
``` VB
Public Property WaitForSyncReplication As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> WaitForSyncReplication {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member WaitForSyncReplication : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## See Also


#### Reference
<a href="8b99144e-5ea9-2e42-3c90-eb4af40d82c9">PostCollectionQuery Class</a><br /><a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models Namespace</a><br />