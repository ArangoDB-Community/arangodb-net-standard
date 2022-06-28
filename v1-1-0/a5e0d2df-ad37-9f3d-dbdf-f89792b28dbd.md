# ViewDetails.PrimarySortCompression Property 
 

Defines how to compress the primary sort data (introduced in v3.7.1). ArangoDB v3.5 and v3.6 always compress the index using LZ4. This option is immutable. Possible values: 1) <a href="aa02d5bb-49fe-79e6-de13-1c473f34860c">LZ4SortCompression</a> (default): use LZ4 fast compression. 2) <a href="5fd254e8-57de-28e9-708c-19a038d4dc52">NoSortCompression</a>: disable compression to trade space for speed. Read more about this in the documentation.

**Namespace:**&nbsp;<a href="23bbeb16-c099-4f2c-4dad-2e67e1a19df4">ArangoDBNetStandard.ViewApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public string PrimarySortCompression { get; set; }
```

**VB**<br />
``` VB
Public Property PrimarySortCompression As String
	Get
	Set
```

**C++**<br />
``` C++
public:
property String^ PrimarySortCompression {
	String^ get ();
	void set (String^ value);
}
```

**F#**<br />
``` F#
member PrimarySortCompression : string with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>

## See Also


#### Reference
<a href="5e40ec8b-d467-c688-72b2-fc3e3e36d569">ViewDetails Class</a><br /><a href="23bbeb16-c099-4f2c-4dad-2e67e1a19df4">ArangoDBNetStandard.ViewApi.Models Namespace</a><br />