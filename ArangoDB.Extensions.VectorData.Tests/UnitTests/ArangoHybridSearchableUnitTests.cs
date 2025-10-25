using Microsoft.Extensions.DependencyInjection;

using NSubstitute.ExceptionExtensions;

using System.Text.Json.Serialization;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

[ExcludeFromCodeCoverage]
public class ArangoHybridSearchableUnitTests
{
    private readonly Faker _faker = new();

    public class TestRecord
    {
        [JsonPropertyName("_key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("embedding")]
        public float[] Embedding { get; set; } = [];
    }

    public class TestRecordWithoutProperties
    {
    }

    [Test]
    public void Constructor_WithNameAndServiceProvider_SetsPropertiesCorrectly()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            searchable.Name.ShouldBe(name);
            searchable.Definition.ShouldBeNull();
        }
    }

    [Test]
    public void Constructor_WithNameDefinitionAndServiceProvider_SetsPropertiesCorrectly()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        VectorStoreCollectionDefinition definition = new();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoHybridSearchable<TestRecord> searchable = new(name, definition, serviceProvider);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            searchable.Name.ShouldBe(name);
            searchable.Definition.ShouldBe(definition);
        }
    }

    [Test]
    public void GetService_WithRegisteredService_ReturnsService()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        services.AddSingleton<IArangoDBClient>(
            new ArangoDBClient(new HttpClient()));
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        IArangoDBClient? result = (IArangoDBClient?)searchable.GetService(typeof(IArangoDBClient));

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ArangoDBClient>();
        if (result is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public void GetService_WithUnregisteredService_ReturnsNull()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(IArangoDBClient));

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public void GetService_WithKeyedService_ReturnsKeyedService()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        string serviceKey = _faker.Lorem.Word();

        // Create a mock service provider that implements IKeyedServiceProvider
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider, IKeyedServiceProvider>();
        object expectedService = new();
        ((IKeyedServiceProvider)serviceProvider)
            .GetRequiredKeyedService(typeof(string), serviceKey)
            .Returns(expectedService);

        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string), serviceKey);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            result.ShouldBe(expectedService);
            ((IKeyedServiceProvider)serviceProvider).Received(1).GetRequiredKeyedService(typeof(string), serviceKey);
        }
    }

    [Test]
    public void GetService_WithServiceKey_WhenInvalidOperationException_ReturnsNull()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        string serviceKey = _faker.Lorem.Word();

        // Create a mock service provider that implements IKeyedServiceProvider
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider, IKeyedServiceProvider>();
        ((IKeyedServiceProvider)serviceProvider)
            .GetRequiredKeyedService(typeof(string), serviceKey)
            .Throws<InvalidOperationException>();

        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string), serviceKey);

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public async Task HybridSearchAsync_WithZeroTop_ReturnsEmpty()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        IAsyncEnumerable<VectorSearchResult<TestRecord>> results = searchable.HybridSearchAsync("test", ["keyword"], 0);

        // Assert
        await foreach (VectorSearchResult<TestRecord> result in results)
        {
            Assert.Fail("Should not return any results");
        }
    }

    [Test]
    public async Task HybridSearchAsync_WithNegativeTop_ReturnsEmpty()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        IAsyncEnumerable<VectorSearchResult<TestRecord>> results = searchable.HybridSearchAsync("test", ["keyword"], -1);

        // Assert
        await foreach (VectorSearchResult<TestRecord> result in results)
        {
            Assert.Fail("Should not return any results");
        }
    }

    [Test]
    public async Task HybridSearchAsync_WithoutVectorProperty_ThrowsArgumentNullException()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        HybridSearchOptions<TestRecord> options = new()
        {
            AdditionalProperty = x => x.Name
        };

        // Act & Assert
        ArgumentNullException exception = await Should.ThrowAsync<ArgumentNullException>(async () =>
        {
            await foreach (VectorSearchResult<TestRecord> result in searchable.HybridSearchAsync("test", ["keyword"], 10, options))
            {
                // Should not reach here
            }
        });

        exception.ParamName.ShouldBe("VectorProperty");
    }

    [Test]
    public async Task HybridSearchAsync_WithoutAdditionalProperty_ThrowsArgumentNullException()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        HybridSearchOptions<TestRecord> options = new()
        {
            VectorProperty = x => x.Embedding
        };

        // Act & Assert
        ArgumentNullException exception = await Should.ThrowAsync<ArgumentNullException>(async () =>
        {
            await foreach (VectorSearchResult<TestRecord> result in searchable.HybridSearchAsync("test", ["keyword"], 10, options))
            {
                // Should not reach here
            }
        });

        exception.ParamName.ShouldBe("AdditionalProperty");
    }

    [Test]
    public async Task HybridSearchAsync_WithoutEmbeddingGenerator_ThrowsInvalidOperationException()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        services.AddSingleton(Substitute.For<IArangoDBClient>());
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        HybridSearchOptions<TestRecord> options = new()
        {
            VectorProperty = x => x.Embedding,
            AdditionalProperty = x => x.Name
        };

        // Act & Assert
        InvalidOperationException exception = await Should.ThrowAsync<InvalidOperationException>(async () =>
        {
            await foreach (VectorSearchResult<TestRecord> result in searchable.HybridSearchAsync("test", ["keyword"], 10, options))
            {
                // Should not reach here
            }
        });

        exception.Message.ShouldContain("IEmbeddingGenerator");
    }

    [Test]
    public void Name_Property_ReturnsConstructorValue()
    {
        // Arrange
        string expectedName = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoHybridSearchable<TestRecord> searchable = new(expectedName, serviceProvider);

        // Assert
        searchable.Name.ShouldBe(expectedName);
    }

    [Test]
    public void Definition_Property_WithDefinition_ReturnsDefinition()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        VectorStoreCollectionDefinition expectedDefinition = new();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoHybridSearchable<TestRecord> searchable = new(name, expectedDefinition, serviceProvider);

        // Assert
        searchable.Definition.ShouldBe(expectedDefinition);
    }

    [Test]
    public void Definition_Property_WithoutDefinition_ReturnsNull()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoHybridSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Assert
        searchable.Definition.ShouldBeNull();
    }
}
