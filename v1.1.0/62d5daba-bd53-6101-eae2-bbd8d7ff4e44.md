# QueryTrackingConfiguration.TrackSlowQueries Property 
 

If set to true, then slow queries will be tracked in the list of slow queries if their runtime exceeds the value set in slowQueryThreshold. In order for slow queries to be tracked, the enabled property must also be set to true.

**Namespace:**&nbsp;<a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? TrackSlowQueries { get; set; }
```

**VB**<br />
``` VB
Public Property TrackSlowQueries As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> TrackSlowQueries {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member TrackSlowQueries : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## See Also


#### Reference
<a href="822307a9-625d-2a71-e3f5-a759e195fc02">QueryTrackingConfiguration Class</a><br /><a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models Namespace</a><br />