using ArangoDBNetStandard.DocumentApi.Models;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

public partial class ArangoCollectionUnitTests
{
    [Test]
    [TestCase("abc", "/abc")]
    public async Task DeleteAsync_ShouldExecute_WhenKeyStartsWithSlash(
        string collectionName,
        string key)
    {
        // Arrange
        DeleteDocumentResponse<object> response = new()
        {
            _key = key,
            _id = $"{collectionName}/{key}",
        };
        _arangoClient
            .Document
            .DeleteDocumentAsync(
                Arg.Any<string>(),
                Arg.Any<DeleteDocumentQuery>(),
                Arg.Any<DocumentHeaderProperties>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(()
            => collection.DeleteAsync(key));
    }

    [Test]
    [TestCase("abc", " abc ")]
    public async Task DeleteAsync_ShouldExecute_WhenKeyContainsStartingOrTrailingSpace(
        string collectionName,
        string key)
    {
        // Arrange
        DeleteDocumentResponse<object> response = new()
        {
            _key = key,
            _id = $"{collectionName}/{key}",
        };
        _arangoClient
            .Document
            .DeleteDocumentAsync(
                Arg.Any<string>(),
                Arg.Any<DeleteDocumentQuery>(),
                Arg.Any<DocumentHeaderProperties>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(()
            => collection.DeleteAsync(key));
    }

    [Test]
    [TestCase("abc", 1)]
    public async Task DeleteAsync_ShouldExecute_WhenKeyIsNotString(
        string collectionName,
        int key)
    {
        // Arrange
        DeleteDocumentResponse<object> response = new()
        {
            _key = key.ToString(),
            _id = $"{collectionName}/{key}",
        };
        _arangoClient
            .Document
            .DeleteDocumentAsync(
                Arg.Any<string>(),
                Arg.Any<DeleteDocumentQuery>(),
                Arg.Any<DocumentHeaderProperties>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<int, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(()
            => collection.DeleteAsync(key));
    }

    [Test]
    [TestCase("abc", "abc")]
    public async Task DeleteAsync_ShouldExecute_WhenKeyIsCombinationOfCollectionNameAndDocumentKey(
       string collectionName,
       string key)
    {
        // Arrange
        DeleteDocumentResponse<object> response = new()
        {
            _key = key,
            _id = $"{collectionName}/{key}",
        };
        _arangoClient
            .Document
            .DeleteDocumentAsync(
                Arg.Any<string>(),
                Arg.Any<DeleteDocumentQuery>(),
                Arg.Any<DocumentHeaderProperties>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        await Should.NotThrowAsync(()
            => collection.DeleteAsync($" {collectionName}/{key} "));
    }

    [Test]
    [TestCase("abc", "abc/")]
    public async Task DeleteAsync_ShouldThrowFormatException_WhenKeyEndsWithSlash(
        string collectionName,
        string key)
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            FormatException exception = await Should.ThrowAsync<FormatException>(()
                => collection.DeleteAsync(key));
            exception.Message.ShouldBe("Key string cannot end with a slash.");
        }
    }

    [Test]
    [TestCase("abc", "abc def")]
    public async Task DeleteAsync_ShouldThrowFormatException_WhenKeyContainsSpace(
        string collectionName,
        string key)
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            collectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            FormatException exception = await Should.ThrowAsync<FormatException>(()
                => collection.DeleteAsync(key));
            exception.Message.ShouldBe("Key cannot contain spaces.");
        }
    }

    [Test]
    [TestCase("")]
    [TestCase("   ")]
    public async Task DeleteAsync_ShouldThrowArgumentException_WhenKeyIsNullOrWhitespace(
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
                => collection.DeleteAsync(key!));
            exception.ParamName.ShouldBe("key");
            exception.Message.ShouldContain("Key can't be null or empty.");
        }
    }

    [Test]
    [TestCase("abc", "abc")]
    public async Task DeleteAsync_ShouldThrowNotSupportedException_WhenKeyContainSlashButCollectionNameDoesNotMatch(
        string collectionName,
        string key)
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            NotSupportedException exception = await Should.ThrowAsync<NotSupportedException>(()
                => collection.DeleteAsync($"{collectionName}/{key}"));
            exception.Message.ShouldBe("A document from another collection can't be accessed.");
        }
    }

    [Test]
    [TestCase("abc", "abc")]
    public async Task DeleteAsync_ShouldThrowFormatException_WhenKeyContainMultipleSlashes(
        string collectionName,
        string key)
    {
        // Arrange
        _serviceProvider
            .GetService(typeof(IArangoDBClient))
            .Returns(_arangoClient);
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            FormatException exception = await Should.ThrowAsync<FormatException>(()
                => collection.DeleteAsync($"{collectionName}/{key}/abc"));
            exception.Message.ShouldBe("The 'Key' can either be the document key or the fully qualified id (CollectionName/DocumentKey).");
        }
    }
}
