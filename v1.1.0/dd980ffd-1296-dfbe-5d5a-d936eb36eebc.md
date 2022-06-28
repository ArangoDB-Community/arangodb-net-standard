# PostCollectionQuery.EnforceReplicationFactor Property 
 

Default is true which means the server will check if there are enough replicas available at creation time and bail out otherwise. Set to false to disable this extra check.

**Namespace:**&nbsp;<a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? EnforceReplicationFactor { get; set; }
```

**VB**<br />
``` VB
Public Property EnforceReplicationFactor As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> EnforceReplicationFactor {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member EnforceReplicationFactor : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## See Also


#### Reference
<a href="8b99144e-5ea9-2e42-3c90-eb4af40d82c9">PostCollectionQuery Class</a><br /><a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models Namespace</a><br />