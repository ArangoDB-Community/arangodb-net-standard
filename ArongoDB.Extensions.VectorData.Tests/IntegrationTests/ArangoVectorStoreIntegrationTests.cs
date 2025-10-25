using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Transport.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

[ExcludeFromCodeCoverage]
public class ArangoVectorStoreIntegrationTests : ArangoDbIntegrationTestBase
{
    // Your test methods go here
    [Test]
    public async Task GetCollection_ShouldReturnCollection_WhenCollectionExists()
    {
        // Act
        using VectorStoreCollection<string, Dictionary<string, object?>> collectionToAssert = VectorStore
            .GetCollection<string, Dictionary<string, object?>>(CollectionName, null);

        // Assert
        GetCollectionResponse getCollectionResponse = await ArangoDbClient.Collection
            .GetCollectionAsync(CollectionName);

        using (Assert.EnterMultipleScope())
        {
            collectionToAssert.ShouldNotBeNull();
            collectionToAssert.Name.ShouldBe(getCollectionResponse.Name);
            collectionToAssert.ShouldBeOfType<ArangoCollection<string, Dictionary<string, object?>>>();
            getCollectionResponse.Code.ShouldBe(HttpStatusCode.OK);
            getCollectionResponse.Error.ShouldBeFalse();
        }
    }

    // Your test methods go here
    [Test]
    public async Task GetDyncamicCollection_ShouldReturnCollection_WhenCollectionExists()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();

        // Act
        using VectorStoreCollection<object, Dictionary<string, object?>> collectionToAssert = store
            .GetDynamicCollection(CollectionName, new VectorStoreCollectionDefinition());

        // Assert
        GetCollectionResponse getCollectionResponse = await ArangoDbClient.Collection
            .GetCollectionAsync(CollectionName);

        using (Assert.EnterMultipleScope())
        {
            collectionToAssert.ShouldNotBeNull();
            collectionToAssert.Name.ShouldBe(getCollectionResponse.Name);
            collectionToAssert.ShouldBeOfType<ArangoDynamicCollection>();
            getCollectionResponse.Code.ShouldBe(HttpStatusCode.OK);
            getCollectionResponse.Error.ShouldBeFalse();
        }
    }

    // Your test methods go here
    [Test]
    public async Task CollectionExistAsync_ShouldReturnTrue_WhenCollectionExists()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();

        // Act
        bool exists = await store
           .CollectionExistsAsync(CollectionName);

        // Assert
        GetCollectionResponse getCollectionResponse = await ArangoDbClient.Collection
            .GetCollectionAsync(CollectionName);

        using (Assert.EnterMultipleScope())
        {
            exists.ShouldBeTrue();
            getCollectionResponse.Error.ShouldBeFalse();
            getCollectionResponse.Code.ShouldBe(HttpStatusCode.OK);
        }
    }

    // Your test methods go here
    [Test]
    public async Task CollectionExistAsync_ShouldReturnFalse_WhenCollectionDoesNotExist()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();
        string nonExistentCollectionName = Faker.Random.String2(5);

        // Act
        bool exists = await store.CollectionExistsAsync(nonExistentCollectionName);

        // Assert
        try
        {
            GetCollectionResponse getCollectionResponse = await ArangoDbClient.Collection
                .GetCollectionAsync(nonExistentCollectionName);
        }
        catch (ApiErrorException ex)
        {
            using (Assert.EnterMultipleScope())
            {
                exists.ShouldBeFalse();
                ex.ShouldBeOfType<ApiErrorException>();
                ex.ApiError.Code.ShouldBe(HttpStatusCode.NotFound);
                ex.ApiError.Error.ShouldBeTrue();
            }
        }
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldDeleteCollection_WhenCollectionExists()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();
        string newCollection = Faker.Random.String2(5);
        await ArangoDbClient.Collection.PostCollectionAsync(new()
        {
            Name = newCollection
        });

        // Act and Assert
        await store.EnsureCollectionDeletedAsync(newCollection);

        // Assert
        try
        {
            GetCollectionResponse getCollectionResponse = await ArangoDbClient.Collection
                .GetCollectionAsync(newCollection);
        }
        catch (ApiErrorException ex)
        {
            using (Assert.EnterMultipleScope())
            {
                ex.ShouldBeOfType<ApiErrorException>();
                ex.ApiError.Code.ShouldBe(HttpStatusCode.NotFound);
                ex.ApiError.Error.ShouldBeTrue();
            }
        }
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldNotThrowException_WhenCollectionDoesNotExist()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();
        string nonExistentCollection = Faker.Random.String2(5);

        // Act and Assert
        await Should.NotThrowAsync(() =>
            store.EnsureCollectionDeletedAsync(nonExistentCollection)
        );
    }

    [Test]
    public async Task ListCollectionNamesAsync_ShouldReturnListOfCollectionNames_WhenCollectionDoesNotExist()
    {
        // Arrange
        VectorStore store = ScopedServiceProvider.GetRequiredService<VectorStore>();
        string nonExistentCollection = Faker.Random.String2(5);
        GetCollectionsResponse getCollectionsResponse = await ArangoDbClient.Collection
            .GetCollectionsAsync();

        // Act and Assert
        List<string> collectionNames = [];
        await foreach (string colName in store.ListCollectionNamesAsync())
        {
            collectionNames.Add(colName);
        }

        // Assert
        using (Assert.EnterMultipleScope())
        {
            collectionNames.ShouldNotBeNull();
            collectionNames.Count.ShouldBe(getCollectionsResponse.Result.Count);
            collectionNames.ShouldBe(
                getCollectionsResponse.Result.Select(r => r.Name),
                caseSensitivity: Case.Sensitive);
        }
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetService_ReturnsServiceWithoutThrowingException_WhenServiceKeyNotProvidedAndProperlyRegistered(
        object? serviceKey)
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        IServiceCollection services = builder.Services;
        HttpApiTransport transport = null!;
        services.AddScoped(_ => CreateArangoDbClient(out transport));
        services.AddArangoVectorDatabase();
        IServiceProvider rootServiceProvider = services.BuildServiceProvider();
        using IServiceScope scope = rootServiceProvider.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        VectorStore vectorStore = serviceProvider.GetRequiredService<VectorStore>();
        object expectedService = serviceProvider.GetRequiredService<VectorStore>();
        Type serviceType = typeof(VectorStore);
        Type expectedServiceType = typeof(ArangoVectorStore);

        // Act and Assert 
        using (Assert.EnterMultipleScope())
        {
            object? actualService = null;
            Should.NotThrow(() =>
                actualService = vectorStore.GetService(serviceType, serviceKey));
            actualService.ShouldNotBeNull();
            actualService.ShouldBe(expectedService);
            actualService.GetType().ShouldBe(expectedServiceType);
        }
        transport.Dispose();
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetService_ReturnsNullWithoutThrowingException_WhenServiceKeyNotProvidedAndNotRegistered(
        object? serviceKey)
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        IServiceCollection services = builder.Services;
        HttpApiTransport transport = null!;
        services.AddScoped(_ => CreateArangoDbClient(out transport));
        services.AddArangoVectorDatabase();
        IServiceProvider rootServiceProvider = services.BuildServiceProvider();
        using IServiceScope scope = rootServiceProvider.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        VectorStore vectorStore = serviceProvider.GetRequiredService<VectorStore>();
        Type serviceType = typeof(ArangoVectorStore);

        // Act 

        // Assert
        using (Assert.EnterMultipleScope())
        {
            object? actualService = null;
            Should.NotThrow(() =>
                actualService = vectorStore.GetService(
                   serviceType,
                   serviceKey));
            actualService.ShouldBeNull();
        }
        transport.Dispose();
    }

    [Test]
    [TestCase("serviceKey1")]
    public async Task GetService_ReturnsServiceWithoutThrowingException_WhenServiceKeyProvidedAndProperlyRegistered(
        object serviceKey)
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        IServiceCollection services = builder.Services;
        HttpApiTransport transport = null!;
        services.AddScoped(_ => CreateArangoDbClient(out transport));
        services.AddArangoVectorDatabase();
        services.AddKeyedScoped<VectorStore, ArangoVectorStore>(serviceKey);
        IServiceProvider rootServiceProvider = services.BuildServiceProvider();
        await using AsyncServiceScope scope = rootServiceProvider.CreateAsyncScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        using VectorStore expectedService = serviceProvider.GetRequiredKeyedService<VectorStore>(serviceKey);
        using VectorStore vectorStore = serviceProvider.GetRequiredKeyedService<VectorStore>(serviceKey);
        Type expectedServiceType = typeof(VectorStore);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            object? actualService = null;
            Should.NotThrow(() =>
            {
                actualService = vectorStore.GetService(expectedServiceType, serviceKey);
                return actualService;
            });
            actualService.ShouldBe(expectedService);
        }
        transport.Dispose();
    }

    [Test]
    [TestCase("serviceKey1")]
    public async Task GetService_ReturnsNullWithoutThrowingException_WhenServiceKeyProvidedAndNotRegistered(
        object serviceKey)
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        IServiceCollection services = builder.Services;
        HttpApiTransport transport = null!;
        services.AddScoped(_ => CreateArangoDbClient(out transport));
        services.AddArangoVectorDatabase();
        IServiceProvider rootServiceProvider = services.BuildServiceProvider();
        await using AsyncServiceScope scope = rootServiceProvider.CreateAsyncScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        using VectorStore vectorStore = serviceProvider.GetRequiredService<VectorStore>();
        Type serviceType = typeof(ArangoVectorStore);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            object? actualService = null;
            Should.NotThrow(() =>
            {
                actualService = vectorStore.GetService(serviceType, serviceKey);
                return actualService;
            });
            actualService.ShouldBeNull();
        }
        transport.Dispose();
    }

    private static IArangoDBClient CreateArangoDbClient(out HttpApiTransport transport)
    {
        Uri baseUri = new($"http://localhost:1234/");
        transport = HttpApiTransport.UsingBasicAuth(
            baseUri,
            "_system",
            string.Empty);
        IArangoDBClient client = new ArangoDBClient(transport, true);
        return client;
    }
}

