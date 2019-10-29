# arangodb-net-standard: The C#/.NET Standard API driver for ArangoDB.

[![CircleCI](https://circleci.com/gh/Actify-Inc/arangodb-net-standard.svg?style=svg)](https://circleci.com/gh/Actify-Inc/arangodb-net-standard)

## Summary

`arangodb-net-standard` is a consistent, comprehensive, minimal driver for the [ArangoDB REST API](https://www.arangodb.com/docs/stable/http/). Built using .NET Standard 2.0, the library provides .NET Core and .NET Framework applications with the complete range of features exposed by the ArangoDB REST API.

The library does not aim to provide much in the way of abstraction on top of the individual REST API functions offered by ArangoDB but it does aim to provide comprehensive coverage of all of the available options for each of ArangoDB's REST API endpoints.

The driver is built using the standard [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netstandard-2.0) interface for making HTTP requests, along with [Json.NET](https://www.newtonsoft.com/json) for (de)serialisation to/from CLI types.

In addition the library provides:

- Support for async/await across every API operation
- Adherence to common-practice C# naming conventions
- Consistent approach for each API endpoint

## Contributing

We welcome any contributions from the wider ArangoDB community. See the [roadmap document](project/roadmap.md) to get an idea of what still needs done. 

Please discuss via issue, email, or any other method with the owners of this repository before working on something so we can keep everyone on the right track.

## Usage

The library  is split into individual classes for each REST API entity.  For example, `DocumentApiClient`, `CollectionApiClient`, etc.  

Each API client class can be instantiated by passing an instance of `IApiClientTransport`. A concrete implementation of this interface is provided, `HttpApiTransport`, which acts as a facade over an instance of `HttpClient`. It is recommended to use a single instance of `HttpApiTransport` in your application in accordance with [the documentation](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.8#remarks) for `HttpClient`.

An instance of `HttpApiTransport` can be created with appropriate base path and basic auth credentials as follows:

```csharp
var transport = new HttpApiTransport(
    new Uri("http://localhost:8529/"),
    dbName,
    "root",
    "root");
```

Once you have an instance of `HttpApiTransport`, you can instantiate an individual API client class, e.g.

```csharp
var documentClient = new DocumentApiClient(httpTransport);
```

The library also offers a composite class which exposes all of the different clients, used as follows:

```csharp
var adb = new ArangoDBClient(httpTransport);
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