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

## Usage

See the [usage](arangodb-net-standard/docs/usage.md) documentation.

## Contributing

We welcome any contributions from the wider ArangoDB community.

- See the [contributing](contributing.md) document.
- See the [roadmap document](project/roadmap.md) to get an idea of what still needs done. 

Please discuss via issue, email, or any other method with the owners of this repository before working on something so we can keep everyone on the right track.
