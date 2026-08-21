using ArangoDBNetStandard.DocumentApi.Models;

using NSubstitute.ExceptionExtensions;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    [TestCase("")]
    [TestCase("   ")]
    public async Task GetAsync_ShouldThrowArgumentException_WhenKeyIsNullOrWhitespace(
        string? key)
    {
        // Arrange
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            ArgumentException exception = await Should.ThrowAsync<ArgumentException>(()
                => collection.GetAsync(key!));
            string paramName = "key";
            exception.ParamName.ShouldBe(paramName);
            exception.Message.ShouldBe($"Key can't be null or empty. (Parameter '{paramName}')");
        }
    }

    [Test]
    public async Task GetAsync_ShouldThrowArgumentException_WhenKeyEndsWithSlash()
    {
        // Arrange
        TestRecord rec = new();
        string key = "abc/";
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            FormatException exception = await Should.ThrowAsync<FormatException>(()
                => collection.GetAsync(key));
            exception.Message.ShouldBe("Key string cannot end with a slash.");
        }
    }

    [Test]
    public async Task GetAsync_ShouldRethrowException_WhenGenericExceptionOccurs()
    {
        // Arrange
        TestRecord rec = new();
        string key = "abc";
        _arangoClient
            .Document
            .GetDocumentAsync<TestRecord>(
                Arg.Any<string>(),
                Arg.Any<DocumentHeaderProperties>(),
                token: Arg.Any<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException());
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(()
            => collection.GetAsync(key));
    }
}
