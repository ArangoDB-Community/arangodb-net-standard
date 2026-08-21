using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

public partial class ArangoCollectionIntegrationTests
{
    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithEqualsMethod()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Equals("TestWithEqualsMethod");
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithEqualsMethod" },
            new () { Name = "TestWithEqualsMethod2" },
            new () { Name = "NothingTestWithEqualsMethod" }
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
            results[0].Name.ShouldBe("TestWithEqualsMethod");
        }
    }
    
    [Test]
    public async Task GetAsyncWithFilter_ShouldReturnTwoRecords_WhenComparingWithEqualsMethodWithStringComparison()
    {
        // Arrange
        VectorStoreCollection<string, TestRecord> collection = VectorStore.GetCollection<string, TestRecord>(
            CollectionName,
            null);
        Expression<Func<TestRecord, bool>> filter = r => r.Name.Equals("testWithEqualsmethodWithComparisonArg", StringComparison.OrdinalIgnoreCase);
        List<TestRecord> expectedRecords =
        [
            new () { Name = "TestWithEqualsMethodWithComparisonArg" },
            new () { Name = "TestWithEqualsMethodWithComparisonArg2" },
            new () { Name = "NothingTestWithEqualsMethodWithComparisonArg" }
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
            results[0].Name.ShouldBe("TestWithEqualsMethodWithComparisonArg");
        }
    }
}
