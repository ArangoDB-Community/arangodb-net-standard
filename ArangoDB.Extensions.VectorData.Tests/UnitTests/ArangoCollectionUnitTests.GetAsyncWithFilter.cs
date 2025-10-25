using System.Linq.Expressions;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    public async Task GetAsync_WithFilter_ShouldReturnEmpty_WhenTopIsZeroOrNegative()
    {
        // Arrange
        ArangoCollection<string, TestRecord> collection = new(_vectorStore, CollectionName, _serviceProvider);
        Expression<Func<TestRecord, bool>> filter = r => r.Name == "Test";
        int top = _faker.Random.Int(max: 0);

        // Act
        List<TestRecord> results = [];
        await foreach (TestRecord record in collection.GetAsync(filter, top))
        {
            results.Add(record);
        }

        // Assert
        results.ShouldBeEmpty();
    }
}
