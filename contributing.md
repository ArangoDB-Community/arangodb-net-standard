# Contribution guide

Here you should find all information needed to contribute to the project. We'll try to keep this short!

## Build

There are two projects:

- arangodb-net-standard (.NET Standard library project)
- arangodb-net-standard.Test (.NET Core test project using xUnit test framework)

To restore required nuget dependencies, run:

```
dotnet restore
```

To build, run:

```
dotnet build
```

To run tests, run:

```
dotnet test
```

To generate a nuget package in the folder `arangodb-net-standard\bin\release`, run:

```
dotnet pack --configuration Release .\arangodb-net-standard\ArangoDBNetStandard.csproj
```

You may also open the Visual Studio solution file and build or run tests from within Visual Studio. The main developers on the project use VS2017 at the moment but VS2019 should work in theory.

## Issues

Use Github issues to report bugs or request enhancements.

Use the [roadmap document](project/roadmap.md) to see what tasks need to be done. Not all of them have issues yet in Github. Create an issue in Github if you plan to work on a new feature from the roadmap document.

## Code conventions

## C# conventions

- use 4 spaces for indentation (no tabs)
- lines should not exceed 120 characters in width
- class names use `PascalCase`
- put each class in a separate file
- class name matches file name
- namespaces use `PascalCase`
- namespaces match relative folder path (e.g. file `MyClass.cs` in folder `Stuff/Things` might declare a class in namespace `MyCo.Stuff.Things`)
- use public properties (e.g. `public string MyProp { get; set; }`), not public fields (e.g. `public string MyProp;`)
- public property names use `PascalCase`
- private members use underscore prefix and `camelCase`, e.g. `_myPrivateField` or `_myPrivateProperty`
- async method names use `Async` suffix, e.g. `public async Task MyMethodAsync() { ... }`
- code blocks put opening brace on a newline (no Egyptian brackets)

## Test conventions

The test project is _arangodb-net-standard.Test_. Its default namespace is `ArangoDBNetStandardTest`.

### Test class naming and location

Test classes in _arangodb-net-standard.Test_ follow the library classes in naming, namespacing and relative filepath.
 e.g.:

```
// file: arangodb-net-standard/Stuff/Things/MyClass.cs
namespace ArangoDBNetStandard.Stuff.Things
{
    public class MyClass
    {
        public async Task MyMethodAsync()
        {
            // implementation goes here...
        }
    }
}

// file: arangodb-net-standard.Test/Stuff/Things/MyClassTest.cs
namespace ArangoDBNetStandardTest.Stuff.Things
{
    public class MyClassTest
    {
        public async Task MyMethodAsync_ShouldSucceed()
        {
            // test code goes here...
        }
    }
}
```

- Test class names use the same name as the class under test, with "Test" suffix.
- Test class should have a relative path matching the class under test
- Test class should have a relative namespace matching the class under test

### Test method names

Test methods follow the following naming pattern:

```
Test_Should<ExpectedBehaviour>[_When<ConditionOrContext>]
```

For example, the following test method names could be used:

- `GetCollections_ShouldSucceed()`
- `GetCollections_ShouldThrow_WhenDatabaseIsOffline()`

## Releases

To create a new release, follow these steps:

1. Pull branch for release from actify-inc repo.
2. Check the version number in ./arangodb-net-standard/ArangoDBNetStandard.csproj file
  - if the version number is incorrect, create a separate commit to change the version number, push it and get it merged before proceeding.
3. Run `dotnet pack --configuration Release ./arangodb-net-standard/ArangoDBNetStandard.csproj`
4. Upload the resultant nuget package via https://www.nuget.org/packages/manage/upload
5. Tag the commit from which the package was produced, using the version string as the tag name. Put the nuget package URL as the tag "message": `git tag -a 1.0.0-alpha01 -m "https://www.nuget.org/packages/ArangoDBNetStandard/1.0.0-alpha01"`
6. Run `git push upstream --tags` to push the tag up to github