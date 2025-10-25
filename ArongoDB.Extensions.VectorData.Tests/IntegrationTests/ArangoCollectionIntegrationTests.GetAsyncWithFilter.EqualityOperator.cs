using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAsyncWithFilterWithEqualityOperator_ShouldReturnExactlyOneRecord_WhenComapringWithEqualityOperator()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name == "TestEqualityOperator";
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestEqualityOperator" },
            new () { Name = "TestEqualityOperator2" },
            new () { Name = "Nothing" }
        ];
        await ArangoDbClient.Document
            .PostDocumentsAsync(
                CollectionName,
                expectedRecords,
                null,
                null,
                null,
                default);

        // Act
        List<TestRecord> results = [];
        await foreach (TestRecord record in collection.GetAsync(filter, 2))
        {
            results.Add(record);
        }

        // Assert
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(1);
            results.ShouldAllBe(r => r.Name != "Nothing");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnNoRecord_WhenComparingWithEqualityOperator()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name == "test";
        List<TestRecord> expectedRecords =
        [
            new () { Name = "Test" },
            new () { Name = "Test2" },
            new () { Name = "Nothing" }
        ];
        await ArangoDbClient.Document
            .PostDocumentsAsync(
                CollectionName,
                expectedRecords,
                null,
                null,
                null,
                default);

        // Act
        List<TestRecord> results = [];
        await foreach (TestRecord record in collection.GetAsync(filter, 2))
        {
            results.Add(record);
        }

        // Assert
        results.Count.ShouldBe(0);
    }
}
