using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithContainsMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("Test");
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
    public async Task GetAsyncWithFilter_ShouldReturnNoRecord_WhenComparingWithContainsMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("test");
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

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithContainsMethodWithStringComparison()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("test", StringComparison.OrdinalIgnoreCase);
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
    public async Task GetAsyncWithFilter_ShouldReturnOneRecord_WhenComapringWithContainsMethodAndSkipProvided()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        FilteredRecordRetrievalOptions<TestRecord> options = new()
        {
            Skip = 1
        };
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("TestContainsWithSkip");
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestContainsWithSkip" },
            new () { Name = "TestContainsWithSkip2" },
            new () { Name = "TestUnknown" }
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
        await foreach (TestRecord record in collection.GetAsync(filter, 2, options))
        {
            results.Add(record);
        }

        // Assert
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(1);
            results[0].Name.ShouldBe("TestContainsWithSkip2");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithContainsMethodAndSortDefinitionProvided()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("Test");

        FilteredRecordRetrievalOptions<TestRecord> options = new()
        {
            OrderBy = def => def.Descending(r => r.Name)
        };
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithContainsMethodAndSortDefinitionProvided" },
            new () { Name = "TestWithContainsMethodAndSortDefinitionProvided2" },
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
        await foreach (TestRecord record in collection.GetAsync(filter, 2, options))
        {
            results.Add(record);
        }

        // Assert
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(2);
            results[0].Name.ShouldBe("TestWithContainsMethodAndSortDefinitionProvided2");
            results[1].Name.ShouldBe("TestWithContainsMethodAndSortDefinitionProvided");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnOneRecord_WhenFilterContainsSortDefinitionAndSkipProvided()
    {
        // Arrange

        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains("TestContainswithSortandSkip");

        FilteredRecordRetrievalOptions<TestRecord> options = new()
        {
            Skip = 1,
            OrderBy = def => def.Descending(r => r.Name)
        };
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestContainswithSortandSkip" },
            new () { Name = "TestContainswithSortandSkip2" },
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
        await foreach (TestRecord record in collection.GetAsync(filter, 2, options))
        {
            results.Add(record);
        }

        // Assert
        using (Assert.EnterMultipleScope())
        {
            results.Count.ShouldBe(1);
            results[0].Name.ShouldBe("TestContainswithSortandSkip");
        }
    }

    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithContainsMethodWithParam()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        string filterText = "TestWithContainsMethodWithParam";
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Contains(filterText);
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithContainsMethodWithParam" },
            new () { Name = "TestWithContainsMethodWithParam2" },
            new () { Name = "NothingWithNotContainsMethodWithParam" }
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
            results[0].Name.ShouldBe("TestWithContainsMethodWithParam");
            results[1].Name.ShouldBe("TestWithContainsMethodWithParam2");
        }
    }
}
