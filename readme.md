# ArangoDB_NET_Standard

The C#/.NET Standard API driver for ArangoDB.

## Project summary

ArangoDB_NET_Standard is a consistent, comprehensive, minimal interface to enable .NET applications to use the complete range of features exposed by the ArangoDB REST API.

The ArangoDB_NET_Standard library does not aim to provide much in the way of abstraction on top of the individual REST API functions offered by ArangoDB but it does aim to provide comprehensive coverage of all of the available options for each of ArangoDB's REST API endpoints.

The driver is built using the standard [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netstandard-2.0) interface for making HTTP requests, along with `Json.NET` for (de)serialisation to/from CLI types.

In addition the library provides:

- Support for async/await across every API operation
- Adherence to common-practice C# naming conventions
- Consistent approach for each API endpoint

## Usage

The ArangoDB_NET_Standard client is split into individual classes for each REST API entity.  For example, `DocumentApiClient`, `CollectionApiClient`, etc.  

Each API client class can be instantiated by passing an instance of `HttpClient`. For use in server applications it is recommended to use the same instance of `HttpClient` for all interactions with an ArangoDB server. In other contexts, you may wish to dispose the client after each operation or set of operations.

An instance of `HttpClient` can be set up with appropriate base path and basic auth credentials as follows:

```csharp
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:8529/_db/" + dbName +"/");
httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(Encoding.ASCII.GetBytes("root:root")));
```

Note the trailing `/` on the Base address - it's important! Without this, relative API paths used by the library won't work.

Once you have an instance of `HttpClient`, you can instantiate an individual Api client class, e.g.

```csharp
var documentClient = new DocumentApiClient(httpClient);
```

The library also offers a composite class which exposes all of the different clients, used as follows:

```csharp
var adb = new ArangoDBClient(httpClient);
```

With an instance of `ArangoDBClient` you can access instances of the individual client classes as properties, such as:

- `adb.Document` (DocumentApiClient)
- `adb.Collection` (CollectionApiClient)
- `adb.Cursor` (CursorApiClient)
- etc

## API conventions

- Each REST endpoint is exposed as a method on an API client class.
- Path parameters are always exposed as string arguments, and will come first in the argument list.
- Where an endpoint expects JSON content, an API model class is used. An instance of the API model will be expected as an argument to the method. This will come after any path arguments.
- Where query parameters can be used, an API model class will be used to capture all possible query parameters. Optional parameters will be exposed as nullable properties. An instance of the class will be accepted, always as the last argument to the method. Any properties left as `null` will not be passed in the query parameters to the ArangoDB API.

So the general pattern used will be something like:

```
apiClient.PostEntity(string pathParam, ModelClass jsonContent, QueryParamClass queryParams);
```