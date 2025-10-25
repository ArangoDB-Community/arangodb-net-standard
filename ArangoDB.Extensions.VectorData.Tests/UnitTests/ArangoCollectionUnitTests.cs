using Microsoft.Extensions.AI;

namespace ArangoDB.Extensions.VectorData.Tests.UnitTests;

[ExcludeFromCodeCoverage]
public partial class ArangoCollectionUnitTests
{
    private const string CollectionName = "test_collection";
    private readonly Faker _faker = new();
    private IServiceProvider _serviceProvider;
    private VectorStore _vectorStore;
    private IArangoDBClient _arangoClient;
    private IDocumentApiClient _documentClient;
    private ICollectionApiClient _collectionClient;
    private ICursorApiClient _cursorClient;
    private IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator;

    public ArangoCollectionUnitTests()
    {
        _vectorStore = Substitute.For<VectorStore>();
        _arangoClient = Substitute.For<IArangoDBClient>();
        _documentClient = Substitute.For<IDocumentApiClient>();
        _collectionClient = Substitute.For<ICollectionApiClient>();
        _cursorClient = Substitute.For<ICursorApiClient>();
        _embeddingGenerator = Substitute.For<IEmbeddingGenerator<string, Embedding<float>>>();

        _arangoClient.Document.Returns(_documentClient);
        _arangoClient.Collection.Returns(_collectionClient);
        _arangoClient.Cursor.Returns(_cursorClient);

        _serviceProvider = Substitute.For<IServiceProvider>();
    }

    [Test]
    public void Constructor_WithStoreAndName_ShouldInitializeCorrectly()
    {
        // Act
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            _serviceProvider);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            collection.Name.ShouldBe(CollectionName);
            collection.Definition.ShouldBeNull();
        }
    }

    [Test]
    [TestCase((string?)null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Constructor_WithStoreButWithoutName_ShouldThrowArgumentNullException(
        string? collectionName)
    {
        // Act and Assert
        using (Assert.EnterMultipleScope())
        {
            ArgumentNullException exception = Should.Throw<ArgumentNullException>(() =>
            {
                ArangoCollection<string, TestRecord> collection = new(
                                _vectorStore,
                                collectionName!,
                                _serviceProvider);
            });

            string paramName = "name";
            exception.Message.ShouldBe($"Collection name cannot be null or empty. (Parameter '{paramName}')");
            exception.ParamName.ShouldBe(paramName);
        }
    }

    [Test]
    public void Constructor_WithDefinition_ShouldInitializeCorrectly()
    {
        // Arrange
        VectorStoreCollectionDefinition definition = new();

        // Act
        ArangoCollection<string, TestRecord> collection = new(
            _vectorStore,
            CollectionName,
            definition,
            _serviceProvider);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            collection.Name.ShouldBe(CollectionName);
            collection.Definition.ShouldBe(definition);
        }
    }

    [OneTimeSetUp]
    public void Setup()
    {
        _vectorStore = Substitute.For<VectorStore>();
        _arangoClient = Substitute.For<IArangoDBClient>();
        _documentClient = Substitute.For<IDocumentApiClient>();
        _collectionClient = Substitute.For<ICollectionApiClient>();
        _cursorClient = Substitute.For<ICursorApiClient>();
        _embeddingGenerator = Substitute.For<IEmbeddingGenerator<string, Embedding<float>>>();

        _arangoClient.Document.Returns(_documentClient);
        _arangoClient.Collection.Returns(_collectionClient);
        _arangoClient.Cursor.Returns(_cursorClient);

        _serviceProvider = Substitute.For<IServiceProvider>();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _vectorStore.Dispose();
        _arangoClient.Dispose();
        _embeddingGenerator.Dispose();
    }

    public class TestRecord
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float[]? Embedding { get; set; }
    }
}
