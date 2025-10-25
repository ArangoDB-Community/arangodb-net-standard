using NSubstitute.ExceptionExtensions;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldRethrowException_WhenOtherGenericExceptionOccurs()
    {
        // Arrange
        string nonExistentCollection = _faker.Random.String2(10);
        _vectorStore
            .EnsureCollectionDeletedAsync(nonExistentCollection)
            .ThrowsAsync<InvalidOperationException>();
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            nonExistentCollection,
            _serviceProvider);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(()
            => collection.EnsureCollectionDeletedAsync());
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldIgnoreException_WhenCollectionDoesNotExist()
    {
        // Arrange
        _vectorStore
            .EnsureCollectionDeletedAsync(Arg.Any<string>())
            .ThrowsAsync(new ApiErrorException(new ApiErrorResponse()
            {
                Code = HttpStatusCode.NotFound,
                ErrorMessage = "collection not found"
            }));
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionDeletedAsync());
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldIgnoreException_WhenCollectionNotFound()
    {
        // Arrange
        _vectorStore
            .EnsureCollectionDeletedAsync(Arg.Any<string>())
            .ThrowsAsync(new ApiErrorException(new ApiErrorResponse()
            {
                ErrorNum = 1203, // ArangoDB error code for "collection not found"
                ErrorMessage = "collection not found"
            }));
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionDeletedAsync());
    }
}
