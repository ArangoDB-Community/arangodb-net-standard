# LinkProperties.InBackground Property 
 

If set to true, then no exclusive lock is used on the source collection during View index creation, so that it remains basically available. This option can be set when adding links. It does not get persisted as it is not a View property, but only a one-off option.

**Namespace:**&nbsp;<a href="23bbeb16-c099-4f2c-4dad-2e67e1a19df4">ArangoDBNetStandard.ViewApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public bool InBackground { get; set; }
```

**VB**<br />
``` VB
Public Property InBackground As Boolean
	Get
	Set
```

**C++**<br />
``` C++
public:
property bool InBackground {
	bool get ();
	void set (bool value);
}
```

**F#**<br />
``` F#
member InBackground : bool with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>

## See Also


#### Reference
<a href="93d84450-61da-a2a6-d3c2-40870e849ae0">LinkProperties Class</a><br /><a href="23bbeb16-c099-4f2c-4dad-2e67e1a19df4">ArangoDBNetStandard.ViewApi.Models Namespace</a><br />