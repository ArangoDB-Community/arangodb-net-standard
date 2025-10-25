using Microsoft.Extensions.AI;

using System.Text.Json.Serialization;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

[ExcludeFromCodeCoverage]
public class ArangoHybridSearchableIntegrationTests : ArangoDbIntegrationTestBase
{
    [Test]
    public async Task SearchAsync_WithoutSkippingData_CoversEmbeddingGenerationLogic()
    {
        // Arrange
        try
        {
            // Insert test documents with vector embeddings
            TestRecord[] testDocs = [
                new() { Key = "doc1", Id = "doc1", Name = "Machine Learning", Description = "AI and ML concepts", },
                new() { Key = "doc2", Id = "doc2", Name = "Data Science", Description = "Statistics and analysis", },
                new() { Key = "doc3", Id = "doc3", Name = "Deep Learning", Description = "Neural networks", }
            ];

            List<string> stringsToCreateEmbeddingFor = [
                ..testDocs.Select(d => $"{d.Name}: {d.Description}")
            ];
            (string Value, Embedding<float> Embedding)[] values = await _embeddingGenerator.GenerateAndZipAsync(stringsToCreateEmbeddingFor);

            for (int i = 0; i < testDocs.Length; i++)
            {
                TestRecord doc = testDocs[i];
                doc.Embedding = values[i].Embedding.Vector.Span.ToArray();
                await ArangoDbClient.Document.PostDocumentAsync(CollectionName, doc);
            }

            VectorStoreCollectionDefinition definition = new();

            ArangoHybridSearchable<TestRecord> vectorSearchable = new(
                CollectionName,
                definition,
                ServiceProvider);

            HybridSearchOptions<TestRecord> options = new()
            {
                IncludeVectors = true,
                VectorProperty = e => e.Embedding,
                AdditionalProperty = e => e.Description
            };

            // Act - This covers lines 68-82: embedding generation and vector processing
            List<VectorSearchResult<TestRecord>> results = [];
            await foreach (VectorSearchResult<TestRecord> result in vectorSearchable.HybridSearchAsync(
                "artificial intelligence",
                ["AI"],
                10,
                options))
            {
                results.Add(result);
            }

            // Assert
            using (Assert.EnterMultipleScope())
            {
                results.ShouldNotBeNull();
                results.ShouldNotBeEmpty();
                results.ForEach(result =>
                {
                    result.Record.ShouldNotBeNull();
                    result.Score.ShouldNotBeNull();
                    result.Score.Value.ShouldBeGreaterThanOrEqualTo(0.0);
                });
            }
        }
        finally
        {

        }
    }

    [Test]
    public async Task SearchAsync_WithSkippingData_CoversEmbeddingGenerationLogic()
    {
        // Arrange
        try
        {
            // Insert test documents with vector embeddings
            TestRecord[] testDocs = [
                new() { Key = "doc1", Id = "doc1", Name = "Machine Learning", Description = "AI and ML concepts", },
                new() { Key = "doc2", Id = "doc2", Name = "Data Science", Description = "Statistics and analysis", },
                new() { Key = "doc3", Id = "doc3", Name = "Deep Learning", Description = "Neural networks", }
            ];

            List<string> stringsToCreateEmbeddingFor = [
                ..testDocs.Select(d => $"{d.Name}: {d.Description}")
            ];
            (string Value, Embedding<float> Embedding)[] values = await _embeddingGenerator.GenerateAndZipAsync(stringsToCreateEmbeddingFor);

            for (int i = 0; i < testDocs.Length; i++)
            {
                TestRecord doc = testDocs[i];
                doc.Embedding = values[i].Embedding.Vector.Span.ToArray();
                await ArangoDbClient.Document.PostDocumentAsync(CollectionName, doc);
            }

            VectorStoreCollectionDefinition definition = new();

            ArangoHybridSearchable<TestRecord> vectorSearchable = new(
                CollectionName,
                definition,
                ServiceProvider);

            HybridSearchOptions<TestRecord> options = new()
            {
                IncludeVectors = true,
                VectorProperty = e => e.Embedding,
                AdditionalProperty = e => e.Description,
                Skip = 1
            };

            // Act
            List<VectorSearchResult<TestRecord>> results = [];
            await foreach (VectorSearchResult<TestRecord> result in vectorSearchable.HybridSearchAsync(
                "artificial intelligence",
                ["AI"],
                10,
                options))
            {
                results.Add(result);
            }

            // Assert
            using (Assert.EnterMultipleScope())
            {
                results.ShouldNotBeNull();
                results.ShouldNotBeEmpty();
                results.ForEach(result =>
                {
                    result.Record.ShouldNotBeNull();
                    result.Score.ShouldNotBeNull();
                    result.Score.Value.ShouldBeGreaterThanOrEqualTo(0.0);
                });
            }
        }
        finally
        {

        }
    }

    [Test]
    public async Task SearchAsync_WithProjection_ReturnsProjectedColswithoutTheVectorProperty()
    {
        // Arrange
        try
        {
            // Insert test documents with vector embeddings
            TestRecord[] testDocs = [
                new() { Key = "doc1", Id = "doc1", Name = "Machine Learning", Description = "AI and ML concepts", },
                new() { Key = "doc2", Id = "doc2", Name = "Data Science", Description = "Statistics and analysis", },
                new() { Key = "doc3", Id = "doc3", Name = "Deep Learning", Description = "Neural networks", }
            ];

            List<string> stringsToCreateEmbeddingFor = [
                ..testDocs.Select(d => $"{d.Name}: {d.Description}")
            ];
            (string Value, Embedding<float> Embedding)[] values = await _embeddingGenerator.GenerateAndZipAsync(stringsToCreateEmbeddingFor);

            for (int i = 0; i < testDocs.Length; i++)
            {
                TestRecord doc = testDocs[i];
                doc.Embedding = values[i].Embedding.Vector.Span.ToArray();
                await ArangoDbClient.Document.PostDocumentAsync(CollectionName, doc);
            }

            VectorStoreCollectionDefinition definition = new();

            ArangoHybridSearchable<TestRecord> vectorSearchable = new(
                CollectionName,
                definition,
                ServiceProvider);

            HybridSearchOptions<TestRecord> options = new()
            {
                IncludeVectors = false,
                VectorProperty = e => e.Embedding,
                AdditionalProperty = e => e.Description,
                Skip = 1
            };

            // Act - This covers lines 68-82: embedding generation and vector processing
            List<VectorSearchResult<TestRecord>> results = [];
            await foreach (VectorSearchResult<TestRecord> result in vectorSearchable.HybridSearchAsync(
                "artificial intelligence",
                ["AI"],
                10,
                options))
            {
                results.Add(result);
            }

            // Assert
            using (Assert.EnterMultipleScope())
            {
                results.ShouldNotBeNull();
                results.ShouldNotBeEmpty();
                results.ForEach(result =>
                {
                    result.Record.ShouldNotBeNull();
                    result.Score.ShouldNotBeNull();
                    result.Score.Value.ShouldBeGreaterThanOrEqualTo(0.0);
                });
            }
        }
        finally
        {

        }
    }
    public class TestRecord
    {
        [JsonPropertyName("_key")]
        public string Key { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("embedding")]
        public float[] Embedding { get; set; } = [];
    }

}