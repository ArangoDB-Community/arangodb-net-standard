using ArangoDBNetStandard.DocumentApi.Models;

using System.Net;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task DeleteAsync_ShouldDeleteDocument_WhenDocumentExists()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore
            .GetCollection<string, TestRecord>(CollectionName, null);
        string key = Guid.NewGuid().ToString();
        TestRecord record = new()
        {
            Key = key,
            Name = Faker.Person.FullName
        };
        PostDocumentResponse<TestRecord> postDocumentResponse = await ArangoDbClient
            .Document
            .PostDocumentAsync(collection.Name, record);

        // Act
        await collection.DeleteAsync(postDocumentResponse._key);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            try
            {
                TestRecord testRecord = await ArangoDbClient
                    .Document
                    .GetDocumentAsync<TestRecord>(
                        postDocumentResponse._id,
                        Arg.Any<DocumentHeaderProperties>(),
                        Arg.Any<CancellationToken>());
            }
            catch (ApiErrorException ex)
            {
                ex.ShouldBeOfType<ApiErrorException>();
                ex.ApiError.Code.ShouldBe(HttpStatusCode.NotFound);
            }
        }
    }

}
