using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Transport.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenAI.Embeddings;

using Testcontainers.ArangoDb;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
[ExcludeFromCodeCoverage]
public abstract class ArangoDbIntegrationTestBase 
{
    protected readonly IServiceCollection _services;
    protected IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator;
    protected HttpApiTransport _transport;
    protected AsyncServiceScope _scope;
    private readonly ArangoDbContainer _arangoDbContainer;

    public ArangoDbIntegrationTestBase()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder
            .Configuration
            .AddUserSecrets<ArangoDbIntegrationTestBase>(true, true)
            .AddEnvironmentVariables();
        _services = builder.Services;
        Faker = new Faker();
        _arangoDbContainer = new ArangoDbBuilder()
            .WithImage("arangodb:latest")
            .WithPortBinding(8529, true)
            .WithEnvironment("ARANGO_NO_AUTH", "1")
            //.WithEnvironment("ARANGO_ROOT_PASSWORD", _rootPassword)
            .Build();
    }

    public string Hostname => _arangoDbContainer.Hostname;
    public int Port => _arangoDbContainer.GetMappedPublicPort();
    public IArangoDBClient ArangoDbClient { get; private set; }
    public string UserName { get; private set; }
    public string DatabaseName { get; private set; }
    public string CollectionName { get; private set; }
    public Faker Faker { get; private set; }
    public ServiceProvider ServiceProvider { get; private set; }
    public IServiceProvider ScopedServiceProvider { get; private set; }
    public VectorStore VectorStore { get; private set; }

    [OneTimeSetUp]
    public virtual async Task InitializeAsync()
    {
        await _arangoDbContainer.StartAsync();
        DatabaseName = Faker.Random.String2(15);
        CollectionName = Faker.Random.String2(15).ToLower();
        ArangoDbClient = CreateArangoDbClient();
        await ArangoDbClient.Database.PostDatabaseAsync(new()
        {
            Name = DatabaseName
        });
        await ArangoDbClient.Collection.PostCollectionAsync(new()
        {
            Name = CollectionName,
            Type = CollectionType.Document,
            WaitForSync = true,
        });
        _services.AddSingleton(ArangoDbClient);
        _services.AddArangoVectorDatabase();
        _services.AddScoped(sp =>
        {
            IConfiguration config = sp.GetRequiredService<IConfiguration>();
            string? apiKey = config["OpenAIKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAIKey configuration is missing.");
            }

            EmbeddingClient embeddingClient = new("text-embedding-3-small", apiKey);

            // Adapt the EmbeddingClient to the IEmbeddingGenerator interface
            IEmbeddingGenerator<string, Embedding<float>> generator = embeddingClient.AsIEmbeddingGenerator();
            return generator;
        });
        ServiceProvider = _services.BuildServiceProvider();
        _scope = ServiceProvider.CreateAsyncScope();
        ScopedServiceProvider = _scope.ServiceProvider;
        VectorStore = ScopedServiceProvider.GetRequiredService<VectorStore>();
        _embeddingGenerator = ScopedServiceProvider.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
    }

    [OneTimeTearDown]
    public virtual async Task DisposeAsync()
    {
        await ArangoDbClient.Database.DeleteDatabaseAsync(DatabaseName);
        ArangoDbClient.Dispose();
        VectorStore.Dispose();
        _embeddingGenerator.Dispose();
        await _scope.DisposeAsync();
        await ServiceProvider.DisposeAsync();
        _services.Clear();
        await _arangoDbContainer.DisposeAsync();
    }

    private IArangoDBClient CreateArangoDbClient()
    {
        Uri baseUri = new($"http://{Hostname}:{Port}/");
        _transport = HttpApiTransport.UsingBasicAuth(
            baseUri,
            "_system",
            string.Empty);
        IArangoDBClient client = new ArangoDBClient(_transport, true);
        return client;
    }
}

