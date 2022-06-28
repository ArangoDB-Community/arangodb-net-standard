# PostIndexBody.Unique Property 
 

Set this property to true to create a unique index. Setting it to false or omitting it will create a non-unique index.

**Namespace:**&nbsp;<a href="215740c9-85fc-74fa-998d-14b49b842d56">ArangoDBNetStandard.IndexApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool? Unique { get; set; }
```

**VB**<br />
``` VB
Public Property Unique As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
public:
property Nullable<bool> Unique {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
member Unique : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## Remarks
The following index types do not support uniqueness, and using the unique attribute with these types may lead to an error: <a href="deb8b60d-171b-eec7-83d8-e0c8c984a1f6">Geo</a>, <a href="b01a7e25-6cea-5b51-c623-33ba102e63f2">FullText</a>. Unique indexes on non-shard keys are not supported in a cluster.

## See Also


#### Reference
<a href="f5a253b1-a29a-4d26-d18f-bf7a5868277f">PostIndexBody Class</a><br /><a href="215740c9-85fc-74fa-998d-14b49b842d56">ArangoDBNetStandard.IndexApi.Models Namespace</a><br />