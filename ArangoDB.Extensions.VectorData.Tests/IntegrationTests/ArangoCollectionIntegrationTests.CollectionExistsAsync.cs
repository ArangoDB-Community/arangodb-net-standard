namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

[TestFixture]
public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task CollectionExistsAsync_ShouldReturnTrue_WhenCollectionExists()
    {
        // Arrange
        string collectionName = Faker.Name.LastName();
        await ArangoDbClient
            .Collection
            .PostCollectionAsync(new ()
            {
                Name = collectionName
            });
        using VectorStoreCollection<string, TestRecord> collection = VectorStore
            .GetCollection<string, TestRecord>(collectionName, null);

        // Act
        bool exists = await collection.CollectionExistsAsync();

        // Assert
        exists.ShouldBeTrue();
    }

    [Test]
    public async Task CollectionExistsAsync_ShouldReturnFalse_WhenCollectionDoesNotExist()
    {
        // Arrange
        string randomCollectionName = Faker.Random.Word().ToLower();
        VectorStoreCollection<string, TestRecord> collection = VectorStore
            .GetCollection<string, TestRecord>(randomCollectionName, null);

        // Act
        bool exists = await collection.CollectionExistsAsync();

        // Assert
        exists.ShouldBeFalse();
    }
}
