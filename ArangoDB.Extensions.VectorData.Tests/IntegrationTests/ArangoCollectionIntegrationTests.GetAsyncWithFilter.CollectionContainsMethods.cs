using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithListContainsMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        List<string> names = ["TestWithListContains", "TestWithListContains2"];
        Expression<Func<TestRecord, bool>> filter = r => names.Contains(r.Name);
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithListContains" },
            new () { Name = "TestWithListContains2" },
            new () { Name = "NothingTestWithListNotContains" }
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
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("TestWithListContains");
            results[1].Name.ShouldBe("TestWithListContains2");
        }
    }


    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithIEnumerableContainsMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        IEnumerable<string> names = ["TestWithIEnumerableContains", "TestWithIEnumerableContains2"];
        Expression<Func<TestRecord, bool>> filter = r => names.Contains(r.Name);
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithIEnumerableContains" },
            new () { Name = "TestWithIEnumerableContains2" },
            new () { Name = "NothingTestWithIEnumerableContains" }
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
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("TestWithIEnumerableContains");
            results[1].Name.ShouldBe("TestWithIEnumerableContains2");
        }
    }
}
