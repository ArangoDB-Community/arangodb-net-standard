using ArangoDBNetStandard.Transport.Http;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute.ExceptionExtensions;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    public void GetService_ShouldReturnService_WhenServiceExists()
    {
        // Arrange
        using IArangoDBClient expectedService = new ArangoDBClient(
            new HttpApiTransport(new(), HttpContentType.VPack));
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(expectedService);
        ArangoCollection<string, TestRecord> collection = new(_vectorStore, CollectionName, _serviceProvider);

        // Act
        object? result = collection.GetService(typeof(IArangoDBClient));

        // Assert
        using (Assert.EnterMultipleScope())
        {
            result.ShouldBe(expectedService);
            result.ShouldBeOfType<ArangoDBClient>();
        }
    }

    [Test]
    public void GetService_ShouldReturnNullWithoutThrowingException_WhenServiceDoesNotExist()
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns((IArangoDBClient?)null);
        ArangoCollection<string, TestRecord> collection = new(_vectorStore, CollectionName, _serviceProvider);

        // Act and Assert
        using (Assert.EnterMultipleScope())
        {
            object? result = Should.NotThrow(()
                => collection.GetService(typeof(IArangoDBClient)));

            result.ShouldBeNull();
        }
    }

    [Test]
    public void GetService_WithServiceKey_ShouldReturnKeyedService_WhenServiceExists()
    {
        // Arrange
        string serviceKey = "test_key";
        using IArangoDBClient expectedService = new ArangoDBClient(
            new HttpApiTransport(new(), HttpContentType.VPack));
        IKeyedServiceProvider keyedServiceProvider = Substitute.For<IKeyedServiceProvider>();
        keyedServiceProvider
            .GetRequiredKeyedService(typeof(IArangoDBClient), serviceKey)
            .Returns(expectedService);
        using ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            keyedServiceProvider);

        // Act
        object? result = collection.GetService(typeof(IArangoDBClient), serviceKey);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            result.ShouldBe(expectedService);
            result.ShouldBeOfType<ArangoDBClient>();
        }
    }

    [Test]
    public void GetService_WithServiceKey_ShouldReturnNull_WhenServiceDoesNotExist()
    {
        // Arrange
        string serviceKey = "test_key";
        IKeyedServiceProvider keyedServiceProvider = Substitute.For<IKeyedServiceProvider>();
        keyedServiceProvider
            .GetRequiredKeyedService(typeof(IArangoDBClient), serviceKey)
            .Throws<InvalidOperationException>();
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore, 
            CollectionName, 
            keyedServiceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            object? result = null;
            Should.NotThrow(() =>
                result = collection.GetService(typeof(IArangoDBClient), serviceKey)
            );

            // Assert
            result.ShouldBeNull();
        }
    }
}
