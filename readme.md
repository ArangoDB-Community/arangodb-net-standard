# arangodb-net-standard: The C#/.NET Standard API driver for ArangoDB.

[![CircleCI](https://circleci.com/gh/ArangoDB-Community/arangodb-net-standard.svg?style=svg)](https://circleci.com/gh/ArangoDB-Community/arangodb-net-standard)

## Description

A consistent, comprehensive, minimal driver for the [ArangoDB REST API](https://www.arangodb.com/docs/stable/http/). Built using .NET Standard 2.0, the library provides .NET Core and .NET Framework applications with the complete range of features exposed by the ArangoDB REST API.

---

- [arangodb-net-standard: The C#/.NET Standard API driver for ArangoDB.](#arangodb-net-standard--the-c--net-standard-api-driver-for-arangodb)
  * [Description](#description)
  * [Project Summary](#project-summary)
  * [Usage](#usage)
    + [Installation](#installation)
    + [Quick Start](#quick-start)
      - [Create a database](#create-a-database)
      - [Create a collection](#create-a-collection)
      - [Create documents](#create-documents)
      - [Side note on document keys](#side-note-on-document-keys)
      - [Run an AQL query](#run-an-aql-query)
      - [Patch a document](#patch-a-document)
      - [Replace a document](#replace-a-document)
    + [Serialization Options](#serialization-options)
    + [API Errors](#api-errors)
    + [Project Conventions](#project-conventions)
      - [Overall project structure](#overall-project-structure)
      - [API Client Method Signatures](#api-client-method-signatures)
    + [Transport](#transport)
      - [`HttpApiTransport`](#-httpapitransport-)
        * [Authentication](#authentication)
          + [Basic Auth](#basic-auth)
          + [JSON Web Token (JWT)](#json-web-token--jwt-)
    + [`ArangoDBClient`](#-arangodbclient-)
    + [Serialization](#serialization)
  * [Contributing](#contributing)

## Project Summary

The library does not aim to provide much in the way of abstraction on top of the individual REST API functions offered by ArangoDB but it does aim to provide comprehensive coverage of all of the available options for each of ArangoDB's REST API endpoints.

The driver is built using the standard [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netstandard-2.0) interface for making HTTP requests, along with [Json.NET](https://www.newtonsoft.com/json) for (de)serialisation to/from CLI types.

In addition the library provides:

- Support for async/await across every API operation
- Adherence to common-practice C# naming conventions
- Consistent approach for each API endpoint

## Usage

### Installation

Install the latest release of [ArangoDBNetStandard on Nuget](https://www.nuget.org/packages/ArangoDBNetStandard).

### Quick Start

The following examples can be read one after the other to form a complete working example.

#### Create a database

```csharp
// You must use the _system database to create databases
using (var systemDbTransport = HttpApiTransport.UsingBasicAuth(
    new Uri("http://localhost:8529/"),
    "_system",
    "root",
    "root"))
{
    var systemDb = new DatabaseApiClient(systemDbTransport);

    // Create a new database with one user.
    await systemDb.PostDatabaseAsync(
        new PostDatabaseBody
        {
            Name = "arangodb-net-standard",
            Users = new List<DatabaseUser>
            {
                new DatabaseUser
                {
                    Username = "jlennon",
                    Passwd = "yoko123"
                }
            }
        });
}
```

Since we only use the system database once to create another separate database, we've wrapped `systemDbTransport` in a `using` block.

In general, if you are connecting to the same database lots of times, you don't want to dispose `HttpApiTransport` until the end of your application's life.

#### Create a collection

```csharp
// Use our new database, with basic auth credentials for the user jlennon.
var transport = HttpApiTransport.UsingBasicAuth(
    new Uri("http://localhost:8529"),
    "arangodb-net-standard",
    "jlennon",
    "yoko123");

var adb = new ArangoDBClient(transport);

// Create a collection in the database
await adb.Collection.PostCollectionAsync(
    new PostCollectionBody
    {
        Name = "MyCollection"
        // A whole heap of other options exist to define key options, 
        // sharding options, etc
    });
```

#### Create documents

```csharp
// Create document in the collection using anonymous type
await adb.Document.PostDocumentAsync(
    "MyCollection",
    new
    {
        MyProperty = "Value"
    });

// Create document in the collection using strong type
await adb.Document.PostDocumentAsync(
    "MyCollection",
    new MyClass
    {
        ItemNumber = 123456,
        Description = "Some item"
    });
```

#### Side note on document keys

The document object must not have any value against a property named `_key` if you expect ArangoDB to generate the document key for you. 

The default serializer options specify that null values will be ignored, so if your class has a `_key` property, you can leave it as `null` when creating a new document.

If you change the serializer options so that `IgnoreNullValues` is `false` then you cannot create a new document using a class the specifies a property named `_key`, because the ArangoDB API will reject the request.

#### Run an AQL query

```csharp
// Run AQL query (create a query cursor)
var response = await adb.Cursor.PostCursorAsync<MyClassDocument>(
    @"FOR doc IN MyCollection 
      FILTER doc.ItemNumber == 123456 
      RETURN doc");

MyClassDocument item = response.Result.First();
```

#### Patch a document

```csharp
// Partially update document
await adb.Document.PatchDocumentAsync<object, object>(
    "MyCollection",
    item._key,
    new { Description = "More description" });
```

#### Replace a document

```csharp
// Fully update document
item.Description = "Some item with some more description";
await adb.Document.PutDocumentAsync(
    $"MyCollection/{item._key}",
    item);
```

### Serialization Options

All API methods that support passing objects of user-specified data types have an optional method argument to pass in custom serialization options.  These options can be used to control the behaviour of the underlying serializer implementation.

The options are passed as an instance of the `ApiClientSerializationOptions` class, which contains options for:

- `UseCamelCasePropertyNames` (boolean, default is `false`)
- `IgnoreNullValues` (boolean, default is `true`)
- `UseStringEnumConversion` (boolean, default is `false`)
- `IgnoreMissingMember` (boolean, default is `true`)

In addition, the default options can be updated, which will affect all subsequent operations that use these options. To set default options, set them on the serializer implementation itself.  For example, if using the supplied `JsonNetApiClientSerialization`:

```
var serializer = new JsonNetApiClientSerialization();
serializer.DefaultOptions.IgnoreNullValues = false;
```

Note: the default values were chosen in line with Json.NET default values as it is the default serializer.

### HTTP Request Headers

APIs that support specifying HTTP request headers have an optional method argument to pass in header values.

For example, to specify a stream transaction ID when creating a document:


```csharp
await adb.Document.PostDocumentAsync(
    "MyCollection",
    new MyClass
    {
        ItemNumber = 123456,
        Description = "Some item"
    },
    new DocumentHeaderProperties()
    {
        TransactionId = "0123456789"
    });
```

### API Errors

Any time an endpoint responds with an HTTP status code which is not a "success" code, an `ApiErrorException` will be thrown.  You may wish to wrap your API calls in a try/catch block, and catch `ApiErrorException` in certain circumstances.

The `ApiErrorException` object contains the `ApiError` property, which will hold an instance of `ApiErrorResponse` with the following structure. ArangoDB has descriptions for the different `ErrorNum` values in their [online documentation](https://www.arangodb.com/docs/stable/appendix-error-codes.html).

```csharp
/// <summary>
/// ArangoDB API error model
/// </summary>
public class ApiErrorResponse
{
    /// <summary>
    /// Whether this is an error response (always true).
    /// </summary>
    public bool Error { get; set; }

    /// <summary>
    /// Error message.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// ArangoDB error number.
    /// See https://www.arangodb.com/docs/stable/appendix-error-codes.html for error numbers and descriptions.
    /// </summary>
    public int ErrorNum { get; set; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public HttpStatusCode Code { get; set; }
}
```

### Project Conventions

The intention for ArangoDB-net-standard is to expose the ArangoDB REST API as faithfully as possible with a minimal amount of abstraction on top. However:

- We do use C# property-naming conventions (using `PascalCase`) when representing ArangoDB objects
- We do provide template methods with type parameters to support convenient (de)serialization of user data such as ArangoDB documents and edge documents
- All methods representing REST API calls are declared `async`

#### Overall project structure

- The library  is split into individual classes for each REST API entity.  For example, `DocumentApiClient`, `CollectionApiClient`, etc.  
- Each API client class can be instantiated by passing an instance of `IApiClientTransport`. A concrete implementation of this interface is provided, `HttpApiTransport`. You may also plug in your own implementation.
- The wrapper class `ArangoDBClient` is instantiated in the same way as the client classes and provides instances of each client class in its properties.
- Each ArangoDB REST endpoint is exposed as a method on the appropriate API client class.

#### API Client Method Signatures

A typical method signature is something like:

```csharp
// This is illustrative, not actually a method in the API
apiClient.PostEntity(string pathParam, PostEntityBody body, PostEntityQuery query = null);
```
- Path parameters are always exposed as string arguments, and will come first in the argument list. e.g. `pathParam` in the example above.
- Where an endpoint expects some body content, an API model class is used. An instance of the API model will be expected as an argument to the method. This will come after any path arguments. e.g. `PostEntityBody` instance in the example above.
- Optional parameters will be exposed as nullable properties. In cases where the body content is an ArangoDB-specific object, properties with `null` value are ignored and not sent in the request to ArangoDB.  In cases where the body content is a user-specified object (e.g. a document or edge document), `null` values are not ignored.
- Where query parameters can be used, a class will be used to capture all possible query parameters. e.g. `PostEntityQuery` instance in the example above.
- The query argument will be left optional, with a default value `null`. If no query argument is provided, no query string is included in the request to ArangoDB.
- Properties of the query class will be nullable and properties of the query class with null values will not be included in the request to ArangoDB.

### Transport

ArangoDB-net-standard uses the `IApiClientTransport` interface to allow for the ability to provide your own transport implementation. This should allow us to develop support for VelocyStream as an alternative transport in future.  There are also other reasons you might want to provide a customised transport, to implement features such as:

- automated retries for failed requests
- automated failover to a backup ArangoDB instance
- load-balancing requests to multiple ArangoDB instances

#### `HttpApiTransport`

The `HttpApiTransport` class implements `IApiClientTransport` and is the standard HTTP transport provided by ArangoDB-net-standard. It supports the Basic Auth and JWT Authentication schemes.

##### Authentication

###### Basic Auth

To create `HttpApiTransport` using Basic Auth supply appropriate base path and credentials to the static `UsingBasicAuth` method as follows:

```csharp
var transport = HttpApiTransport.UsingBasicAuth(
    new Uri("http://localhost:8529/"),
    dbName,
    "username",
    "password");
```

###### JSON Web Token (JWT)

To create `HttpApiTransport` using JWT tokens, supply appropriate base path and JWT token to the static `UsingJWTAuth` method as follows:

```csharp
var transport = HttpApiTransport.UsingJwtAuth(
    new Uri("http://localhost:8529/"),
    dbName,
    jwtTokenString);
```

This assumes you already _have_ a JWT token. If you need to get a token from ArangoDB, you'll need to first setup an `HttpApiTransport` without any credentials, submit a request to ArangoDB to get a JWT token, then call `SetJwtToken` on the transport. e.g.:

```csharp
// Create HttpApiTransport with no authentication set
var transport = HttpApiTransport.UsingNoAuth(
  new Uri(arangodbBaseUrl),
  databaseName);
  
// Create AuthApiClient using the no-auth transport
var authClient = new AuthApiClient(transport);

// Get JWT token by submitting credentials
var jwtTokenResponse = await authClient.GetJwtTokenAsync("username", "password");

// Set token in current transport
transport.SetJwtToken(jwtTokenResponse.Jwt);

// Use the transport, which will now be authenticated using the JWT token. e.g.:
var databaseApi = new DatabaseApiClient(transport);
var userDatabasesResponse = await databaseApi.GetUserDatabasesAsync();
```

Depending on your application's needs, you might want to securely store the token somewhere so you can use it again later. ArangoDB's tokens are valid for a one month duration, so you will need to get a new token at some point. You must handle fetching new tokens and setting them on the transport instance as part of your application.

### `ArangoDBClient`

ArangoDBClient is a wrapper around all of the individual API client classes. By instantiating ArangoDBClient once, you have access to instances of each API client class.  With an instance of `IApiClientTransport`, create `ArangoDBClient` like so:

```csharp
var adb = new ArangoDBClient(transport);
```

Now you can access instances of the individual client classes as properties, e.g.:

```csharp

// Create a new collection named "TestCollection"
var postCollectionResponse = await adb.Collection.PostCollectionAsync(
  new PostCollectionBody {
    Name = "TestCollection"
  });

// Add a document to "TestCollection"
var docResponse = await adb.Document.PostDocumentAsync(
  "TestCollection", 
  new { TestKey = "TestValue" });

```

### Serialization

ArangoDB-net-standard allows for alternative serializer implementations to be used by implementing the `IApiClientSerialization` interface or `ApiClientSerialization` abstract class. The abstract class provides an additional property for default serialization options to use as fallback when none are provided by the caller. See [Serialization Options](#serialization-options) section above.

By default, all API clients will use the provided `JsonNetApiClientSerialization` which uses the Json.NET library. To use an alternative serialization implementation, pass an instance of `IApiClientSerialization` when instantiating any API client class or the `ArangoDBClient` class.

In many cases we depend on the behaviour of Json.NET to automatically map JSON properties using `camelCase` to C# properties defined using `PascalCase` when deserializing. Any alternative serializer will need to mimic that behaviour in order to deserialize some ArangoDB JSON objects to their C# types.  For example, if using `System.Text.Json`, the option `PropertyNameCaseInsensitive = true` should be used.

If any error occurs during (de)serialization of success/error responses, an exception of type `ArangoDBNetStandard.Serialization.SerializationException` will be thrown. Detailed reasons for the error will be available through the `InnerException` property of this object.

## Contributing

We welcome any contributions from the wider ArangoDB community.

- See the [contributing](contributing.md) document.
- See the [roadmap document](project/roadmap.md) to get an idea of what still needs done. 

Please discuss via issue, email, or any other method with the owners of this repository before working on something so we can keep everyone on the right track.
