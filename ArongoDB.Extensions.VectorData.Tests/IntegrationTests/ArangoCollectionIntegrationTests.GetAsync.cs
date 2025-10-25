using ArangoDBNetStandard.DocumentApi.Models;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAysnc_ReturnsDocument_WhenDocumentExists()
    {
        // Arrange
        string documentName = Faker.Random.Word();
        TestRecord rec = new()
        {
            Name = documentName,
        };
        PostDocumentResponse<TestRecord> newDoc = await ArangoDbClient
            .Document
            .PostDocumentAsync(CollectionName, rec);

        using var collection = VectorStore.GetCollection<string, TestRecord>(CollectionName, null);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            TestRecord? fetchedDoc = null;
            await Should.NotThrowAsync(async () => fetchedDoc = await collection.GetAsync(newDoc._id));
            fetchedDoc.ShouldNotBeNull();
            fetchedDoc.Name.ShouldBe(documentName);
        }
    }

    [Test]
    public async Task GetAysnc_ReturnsNullWithoutThrowingException_WhenDocumentDoesNotExist()
    {
        // Arrange
        string documentName = Faker.Name.LastName();
        TestRecord rec = new()
        {
            Name = documentName,
        };
        string id=$"{CollectionName}/{Faker.Name.LastName()}";

        using var collection = VectorStore.GetCollection<string, TestRecord>(CollectionName, null);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            TestRecord? fetchedDoc = null;
            await Should.NotThrowAsync(async () =>
            {
                fetchedDoc = await collection.GetAsync(id);
            });
            fetchedDoc.ShouldBeNull();
        }
    }
}
