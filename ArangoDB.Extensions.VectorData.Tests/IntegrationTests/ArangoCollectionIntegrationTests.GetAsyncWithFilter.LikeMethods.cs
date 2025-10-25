using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithLikeMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => AqlFilters.Like(r.Name, "Test");
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
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("Test");
            results[1].Name.ShouldBe("Test2");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComapringWithExtensionMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Like("Test");
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
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("Test");
            results[1].Name.ShouldBe("Test2");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComapringWithLikeMethodWithStringComparison()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => AqlFilters.Like(r.Name, "test", StringComparison.OrdinalIgnoreCase);
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
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("Test");
            results[1].Name.ShouldBe("Test2");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithLikeMethodWithStringComparison()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Like("test", StringComparison.OrdinalIgnoreCase);
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
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("Test");
            results[1].Name.ShouldBe("Test2");
        }
    }
}
