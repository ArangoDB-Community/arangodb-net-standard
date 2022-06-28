# QueryTrackingConfiguration.MaxSlowQueries Property 
 

The maximum number of slow queries to keep in the list of slow queries. If the list of slow queries is full, the oldest entry in it will be discarded when additional slow queries occur.

**Namespace:**&nbsp;<a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public int? MaxSlowQueries { get; set; }
```

**VB**<br />
``` VB
Public Property MaxSlowQueries As Integer?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<int> MaxSlowQueries {
	Nullable<int> get ();
	void set (Nullable<int> value);
}
```

**F#**<br />
``` F#
member MaxSlowQueries : Nullable<int> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a>)

## See Also


#### Reference
<a href="822307a9-625d-2a71-e3f5-a759e195fc02">QueryTrackingConfiguration Class</a><br /><a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models Namespace</a><br />