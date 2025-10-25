using Microsoft.Extensions.AI;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    public async Task SearchAsync_ShouldThrowInvalidOperationException_WhenEmbeddingGeneratorNotFound()
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IEmbeddingGenerator<string, Embedding<float>>))
            .Returns((IEmbeddingGenerator<string, Embedding<float>>?)null);
        ArangoCollection<string, TestRecord> collection = new(_vectorStore, CollectionName, _serviceProvider);
        string searchValue = "test search";

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            InvalidOperationException exception = await Should.ThrowAsync<InvalidOperationException>(async () =>
            {
                await foreach (VectorSearchResult<TestRecord> result in collection.SearchAsync(searchValue, 1))
                {
                    // Should throw before any iteration
                }
            });
            exception.Message.ShouldContain("Vector search requires options.EmbeddingGenerator");
        }
    }
}
