# PostCollectionBody.IsVolatile Property 
 

Deprecated. If true then the collection data is kept in-memory only and not made persistent. Unloading the collection will cause the collection data to be discarded. Stopping or re-starting the server will also cause full loss of data in the collection. Setting this option will make the resulting collection be slightly faster than regular collections because ArangoDB does not enforce any synchronization to disk and does not calculate any CRC checksums for datafiles (as there are no datafiles). This option should therefore be used for cache-type collections only, and not for data that cannot be re-created otherwise. (The default is false) This option is meaningful for the MMFiles storage engine only.

**Namespace:**&nbsp;<a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? IsVolatile { get; set; }
```

**VB**<br />
``` VB
Public Property IsVolatile As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> IsVolatile {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member IsVolatile : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## See Also


#### Reference
<a href="dd01270d-520a-693d-96e1-5bb9ef28eb24">PostCollectionBody Class</a><br /><a href="eddef630-2e74-9b99-ee5b-91305adea48b">ArangoDBNetStandard.CollectionApi.Models Namespace</a><br />