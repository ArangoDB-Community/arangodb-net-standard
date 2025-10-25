using ArangoDBNetStandard.CollectionApi.Models;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldDeleteCollection_WhenCollectionExists()
    {
        // Arrange
        string collectionName = Faker.Random.String2(10);
        await ArangoDbClient
            .Collection
            .PostCollectionAsync(new()
            {
                Name = collectionName,
                Type = CollectionType.Document
            });
        VectorStoreCollection<string, TestRecord> collection = VectorStore
            .GetCollection<string, TestRecord>(collectionName, null);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionDeletedAsync());
    }

    [Test]
    public async Task EnsureCollectionDeletedAsync_ShouldIgnoreException_WhenDocumentDoesNotExist()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore
            .GetCollection<string, TestRecord>(Faker.Random.String2(10), null);

        // Act & Assert
        await Should.NotThrowAsync(() => collection.EnsureCollectionDeletedAsync());
    }
}
