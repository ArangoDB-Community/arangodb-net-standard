# PostCursorOptions.Profile Property 
 

If set to true or 1, then the additional query profiling information will be returned in the sub-attribute profile of the extra return attribute, if the query result is not served from the query cache. Set to 2 the query will include execution stats per query plan node in sub-attribute stats.nodes of the extra return attribute. Additionally the query plan is returned in the sub-attribute extra.plan.

**Namespace:**&nbsp;<a href="35799343-7a53-6c3b-95d1-21ff990d1b8b">ArangoDBNetStandard.CursorApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public int? Profile { get; set; }
```

**VB**<br />
``` VB
Public Property Profile As Integer?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<int> Profile {
	Nullable<int> get ();
	void set (Nullable<int> value);
}
```

**F#**<br />
``` F#
member Profile : Nullable<int> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a>)

## See Also


#### Reference
<a href="33e10911-ea6c-31b3-60fc-c57350209014">PostCursorOptions Class</a><br /><a href="35799343-7a53-6c3b-95d1-21ff990d1b8b">ArangoDBNetStandard.CursorApi.Models Namespace</a><br />