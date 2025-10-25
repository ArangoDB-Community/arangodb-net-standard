using Microsoft.Extensions.DependencyInjection;

using NSubstitute.ExceptionExtensions;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

[ExcludeFromCodeCoverage]
public class ArangoVectorSearchableUnitTests
{
    private readonly Faker _faker = new();

    public class TestRecord
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float[] Embedding { get; set; } = [];
    }

    public class TestRecord2
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
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

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
        ArangoVectorSearchable<TestRecord> searchable = new(name, definition, serviceProvider);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            searchable.Name.ShouldBe(name);
            searchable.Definition.ShouldBe(definition);
        }
    }

    [Test]
    public void GetService_WithoutServiceKey_CallsServiceProviderGetService()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
        object expectedService = new();
        serviceProvider.GetService(typeof(string)).Returns(expectedService);
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string));

        // Assert
        using (Assert.EnterMultipleScope())
        {
            result.ShouldBe(expectedService);
            serviceProvider.Received(1).GetService(typeof(string));
        }
    }

    [Test]
    public void GetService_WithServiceKey_CallsServiceProviderGetRequiredKeyedService()
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

        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

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

        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string), serviceKey);

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public async Task SearchAsync_WithZeroTop_YieldsNoResults()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act
        List<VectorSearchResult<TestRecord>> results = [];
        await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 0))
        {
            results.Add(result);
        }

        // Assert
        results.ShouldBeEmpty();
    }

    [Test]
    public async Task SearchAsync_WithNegativeTop_YieldsNoResults()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act
        List<VectorSearchResult<TestRecord>> results = [];
        await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, -5))
        {
            results.Add(result);
        }

        // Assert
        results.ShouldBeEmpty();
    }

    [Test]
    public async Task SearchAsync_WithoutArangoDBClient_ThrowsInvalidOperationException()
    {
        // Arrange
        string name = _faker.Lorem.Word();

        // Create a service collection without required services
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            InvalidOperationException exception = await Should.ThrowAsync<InvalidOperationException>(async () =>
            {
                VectorSearchOptions<TestRecord> options = new()
                {
                    VectorProperty = x => x.Embedding // Provide VectorProperty to pass null check
                };
                await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 5, options))
                {
                    // This should throw before yielding any results
                }
            });

            exception.Message.ShouldContain("No service for type");
        }
    }

    [Test]
    public async Task SearchAsync_WithoutEmbeddingGenerator_ThrowsInvalidOperationException()
    {
        // Arrange
        string name = _faker.Lorem.Word();

        // Create a service collection with client but without embedding generator
        ServiceCollection services = new();
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        services.AddSingleton(client);

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            InvalidOperationException exception = await Should.ThrowAsync<InvalidOperationException>(async () =>
            {
                VectorSearchOptions<TestRecord> options = new()
                {
                    VectorProperty = x => x.Embedding // Provide VectorProperty to pass null check
                };
                await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 5, options))
                {
                    // This should throw before yielding any results
                }
            });

            exception.Message.ShouldContain("Vector search requires options.EmbeddingGenerator implementing IEmbeddingGenerator");
        }
    }

    [Test]
    public void Name_Property_ReturnsCorrectValue()
    {
        // Arrange
        string expectedName = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoVectorSearchable<TestRecord> searchable = new(expectedName, serviceProvider);

        // Assert
        searchable.Name.ShouldBe(expectedName);
    }

    [Test]
    public void Definition_Property_WithDefinition_ReturnsCorrectValue()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        VectorStoreCollectionDefinition expectedDefinition = new();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoVectorSearchable<TestRecord> searchable = new(name, expectedDefinition, serviceProvider);

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
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Assert
        searchable.Definition.ShouldBeNull();
    }

    [Test]
    public void GetService_WithNullServiceKey_ReturnsServiceFromProvider()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        string testService = "test-service";
        services.AddSingleton(testService);
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string));

        // Assert
        result.ShouldBe(testService);
    }

    [Test]
    public void GetService_WithNonExistentService_ReturnsNull()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string));

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public async Task SearchAsync_WithVectorSearchOptions_AcceptsGenericOptions()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act & Assert - This should compile without issues, demonstrating the correct generic signature
        using (Assert.EnterMultipleScope())
        {
            InvalidOperationException exception = await Should.ThrowAsync<InvalidOperationException>(async () =>
            {
                VectorSearchOptions<TestRecord> options = new()
                {
                    VectorProperty = x => x.Embedding // Provide VectorProperty to pass null check
                };

                await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 5, options))
                {
                    // This should throw before yielding any results due to missing dependencies
                }
            });

            exception.Message.ShouldContain("No service for type");
        }
    }

    [Test]
    public void Name_Property_IsImmutableAfterConstruction()
    {
        // Arrange
        string expectedName = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Act
        ArangoVectorSearchable<TestRecord> searchable = new(expectedName, serviceProvider);
        string retrievedName1 = searchable.Name;
        string retrievedName2 = searchable.Name;

        // Assert - Name should be consistent and immutable
        using (Assert.EnterMultipleScope())
        {
            retrievedName1.ShouldBe(expectedName);
            retrievedName2.ShouldBe(expectedName);
            retrievedName1.ShouldBe(retrievedName2);
        }
    }

    [Test]
    public void Constructor_WithDifferentServiceProviders_MaintainsIndependentState()
    {
        // Arrange
        string name1 = _faker.Lorem.Word();
        string name2 = _faker.Lorem.Word();
        ServiceCollection services1 = new();
        ServiceCollection services2 = new();

        using ServiceProvider serviceProvider1 = services1.BuildServiceProvider();
        using ServiceProvider serviceProvider2 = services2.BuildServiceProvider();

        // Act
        ArangoVectorSearchable<TestRecord> searchable1 = new(name1, serviceProvider1);
        ArangoVectorSearchable<TestRecord> searchable2 = new(name2, serviceProvider2);

        // Assert - Each instance should maintain its own state
        using (Assert.EnterMultipleScope())
        {
            searchable1.Name.ShouldBe(name1);
            searchable2.Name.ShouldBe(name2);
            searchable1.Name.ShouldNotBe(searchable2.Name);
        }
    }

    [Test]
    public void GetService_ServiceProviderDisposed_ThrowsObjectDisposedException()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        serviceProvider.Dispose();

        // Assert
        Assert.Throws<ObjectDisposedException>(() => searchable.GetService(typeof(string)));
    }

    [Test]
    public async Task SearchAsync_WithOptionsButNoTop_YieldsNoResults()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        VectorSearchOptions<TestRecord> options = new();

        // Act
        List<VectorSearchResult<TestRecord>> results = [];
        await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 0, options))
        {
            results.Add(result);
        }

        // Assert
        results.ShouldBeEmpty();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-10)]
    [TestCase(-100)]
    public async Task SearchAsync_WithNegativeTopValues_YieldsNoResults(int negativeTop)
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        // Act
        List<VectorSearchResult<TestRecord>> results = [];
        await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, negativeTop))
        {
            results.Add(result);
        }

        // Assert
        results.ShouldBeEmpty();
    }

    [Test]
    public void GetService_WithKeyedServiceProvider_ReturnsCorrectService()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        string serviceKey = _faker.Lorem.Word();
        string expectedService = _faker.Lorem.Sentence();

        ServiceCollection services = new();
        services.AddKeyedSingleton(serviceKey, expectedService);
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);

        // Act
        object? result = searchable.GetService(typeof(string), serviceKey);

        // Assert
        result.ShouldBe(expectedService);
    }

    [Test]
    public async Task SearchAsync_WithNullVectorProperty_ThrowsArgumentNullException()
    {
        // Arrange
        string name = _faker.Lorem.Word();
        ServiceCollection services = new();
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        services.AddSingleton(client);
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        ArangoVectorSearchable<TestRecord> searchable = new(name, serviceProvider);
        string searchValue = _faker.Lorem.Sentence();

        VectorSearchOptions<TestRecord> options = new()
        {
            VectorProperty = null // This should cause ArgumentNullException
        };

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            ArgumentNullException exception = await Should.ThrowAsync<ArgumentNullException>(async () =>
            {
                await foreach (VectorSearchResult<TestRecord> result in searchable.SearchAsync(searchValue, 5, options))
                {
                    // Should not reach here
                }
            });

            exception.ParamName.ShouldBe("VectorProperty");
            exception.Message.ShouldContain("VectorProperty must be specified for vector search operations.");
        }
    }
}
