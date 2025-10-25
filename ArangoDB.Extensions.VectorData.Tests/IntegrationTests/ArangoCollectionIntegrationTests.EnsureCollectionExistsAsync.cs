using ArangoDBNetStandard.CollectionApi.Models;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task EnsureCollectionExistsAsync_ShouldCreateCollection_WhenDoesNotExist()
    {
        // Arrange
        string collectionName = Faker.Random.String2(10);
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            collectionName,
            null);

        // Act

        // Assert
        using (Assert.EnterMultipleScope())
        {
            await Should.NotThrowAsync(()
                => collection.EnsureCollectionExistsAsync());
            GetCollectionResponse getCollectionResponse = await ArangoDbClient
                .Collection
                .GetCollectionAsync(collectionName);
            getCollectionResponse.Name.ShouldBe(collectionName);
            getCollectionResponse.Type.ShouldBe(CollectionType.Document);
            getCollectionResponse.Code.ShouldBe(HttpStatusCode.OK);
        }
    }


    [Test]
    public async Task EnsureCollectionExistsAsync_ShouldIgnoreException_WhenAlreadyExists()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);

        // Act & Assert
        await Should.NotThrowAsync(()
            => collection.EnsureCollectionExistsAsync());
    }
}
