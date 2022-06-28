# PostCursorOptions.Stream Property 
 

Specify true and the query will be executed in a streaming fashion. The query result is not stored on the server, but calculated on the fly. Beware: long-running queries will need to hold the collection locks for as long as the query cursor exists. When set to false a query will be executed right away in its entirety. In that case query results are either returned right away (if the result set is small enough), or stored on the arangod instance and accessible via the cursor API (with respect to the ttl). It is advisable to only use this option on short-running queries or without exclusive locks (write-locks on MMFiles). Please note that the query options cache, count and fullCount will not work on streaming queries. Additionally query statistics, warnings and profiling data will only be available after the query is finished. The default value is false

**Namespace:**&nbsp;<a href="35799343-7a53-6c3b-95d1-21ff990d1b8b">ArangoDBNetStandard.CursorApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? Stream { get; set; }
```

**VB**<br />
``` VB
Public Property Stream As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> Stream {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member Stream : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## See Also


#### Reference
<a href="33e10911-ea6c-31b3-60fc-c57350209014">PostCursorOptions Class</a><br /><a href="35799343-7a53-6c3b-95d1-21ff990d1b8b">ArangoDBNetStandard.CursorApi.Models Namespace</a><br />