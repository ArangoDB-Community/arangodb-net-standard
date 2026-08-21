using NSubstitute.ExceptionExtensions;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

[ExcludeFromCodeCoverage]
public class ArangoVectorStoreUnitTests
{
    [Test]
    public async Task CollectionExistAsync_ThrowsApiErrorException_WhenFound()
    {
        // Arrange
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
        ArangoVectorStore arangoVectorStore = new(client, serviceProvider);
        client
            .Collection
            .GetCollectionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ApiErrorException()
            {
                ApiError = new ApiErrorResponse()
                {
                    Code = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Bad Request"
                }
            });

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            ApiErrorException apiErrorException = await Should.ThrowAsync<ApiErrorException>(
                () => arangoVectorStore.CollectionExistsAsync(null!));
            apiErrorException.ApiError.ShouldNotBeNull();
        }
    }

    [Test]
    public async Task CollectionExistAsync_ThrowsException_WhenOtherExceptionOccured()
    {
        // Arrange
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
        ArangoVectorStore arangoVectorStore = new(client, serviceProvider);
        client
            .Collection
            .GetCollectionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ThrowsAsync<Exception>();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            Exception exception = await Should.ThrowAsync<Exception>(
                () => arangoVectorStore.CollectionExistsAsync(null!));
            exception.ShouldNotBeOfType<ApiErrorException>();
        }
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ThrowsApiErrorException_WhenFound()
    {
        // Arrange
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
        ArangoVectorStore arangoVectorStore = new(client, serviceProvider);
        client
            .Collection
            .DeleteCollectionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ApiErrorException()
            {
                ApiError = new ApiErrorResponse()
                {
                    Code = HttpStatusCode.BadRequest,
                    ErrorMessage = "Bad Request"
                }
            });

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            ApiErrorException apiErrorException = await Should.ThrowAsync<ApiErrorException>(
                () => arangoVectorStore.EnsureCollectionDeletedAsync(null!));
            apiErrorException.ApiError.ShouldNotBeNull();
        }
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ThrowsException_WhenOtherExceptionOccured()
    {
        // Arrange
        IArangoDBClient client = Substitute.For<IArangoDBClient>();
        IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
        ArangoVectorStore arangoVectorStore = new(client, serviceProvider);
        client
            .Collection
            .DeleteCollectionAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ThrowsAsync<Exception>();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            Exception exception = await Should.ThrowAsync<Exception>(
                () => arangoVectorStore.EnsureCollectionDeletedAsync(null!));
            exception.ShouldNotBeOfType<ApiErrorException>();
        }
    }
}
