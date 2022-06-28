# IIndexResponse.GeoJson Property 
 

Supported by indexes of type <a href="deb8b60d-171b-eec7-83d8-e0c8c984a1f6">Geo</a>. If a geo-spatial index on a location is constructed and geoJson is true, then the order within the array is longitude followed by latitude.

**Namespace:**&nbsp;<a href="215740c9-85fc-74fa-998d-14b49b842d56">ArangoDBNetStandard.IndexApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
bool? GeoJson { get; set; }
```

**VB**<br />
``` VB
Property GeoJson As Boolean?
	Get
	Set
```

**C++**<br />
``` C++
property Nullable<bool> GeoJson {
	Nullable<bool> get ();
	void set (Nullable<bool> value);
}
```

**F#**<br />
``` F#
abstract GeoJson : Nullable<bool> with get, set

```


#### Property Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.nullable-1" target="_blank" rel="noopener noreferrer">Nullable</a>(<a href="https://docs.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)

## Remarks
This corresponds to the format described in http://geojson.org/geojson-spec.html#positions

## See Also


#### Reference
<a href="800d84d0-0548-342e-e0fa-e82a2bab7246">IIndexResponse Interface</a><br /><a href="215740c9-85fc-74fa-998d-14b49b842d56">ArangoDBNetStandard.IndexApi.Models Namespace</a><br />