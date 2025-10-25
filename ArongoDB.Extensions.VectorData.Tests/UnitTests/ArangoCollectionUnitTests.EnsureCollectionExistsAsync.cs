using ArangoDBNetStandard.CollectionApi.Models;

using NSubstitute.ExceptionExtensions;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    public async Task EnsureCollectionExistsAsync_ShouldIgnoreException_WhenAlreadyExists()
    {
        // Arrange
        _arangoClient
            .Collection
            .PostCollectionAsync(
                Arg.Any<PostCollectionBody>(),
                null,
                token: Arg.Any<CancellationToken>())
            .ThrowsAsync(new ApiErrorException(new ApiErrorResponse()
            {
                Code = HttpStatusCode.Conflict,
                ErrorMessage = "collection already exists"
            }));
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionExistsAsync());
    }

    [Test]
    public async Task EnsureCollectionExistsAsync_ShouldIgnoreException_WhenAlreadyExistsWithErrorNum()
    {
        // Arrange
        _arangoClient
            .Collection
            .PostCollectionAsync(
                Arg.Any<PostCollectionBody>(),
                token: Arg.Any<CancellationToken>())
            .ThrowsAsync(new ApiErrorException(new ApiErrorResponse()
            {
                ErrorNum = 1207,
                ErrorMessage = "collection already exists"
            }));
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionExistsAsync());
    }

    [Test]
    public async Task EnsureCollectionExistsAsync_ShouldThrowException_WhenGenericExceptionOccurs()
    {
        // Arrange
        _arangoClient
            .Collection
            .PostCollectionAsync(
                Arg.Any<PostCollectionBody>(),
                token: Arg.Any<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException());
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(()
            => collection.EnsureCollectionExistsAsync());
    }
}
