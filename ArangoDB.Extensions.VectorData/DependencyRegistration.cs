using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;

namespace ArangoDB.Extensions.VectorData;

public static class DependencyRegistration
{
    public static IServiceCollection AddArangoVectorDatabase(
        this IServiceCollection services)
    {
        services.AddScoped<VectorStore, ArangoVectorStore>();
        services.AddScoped(typeof(IVectorSearchable<>), typeof(ArangoVectorSearchable<>));
        services.AddScoped(typeof(IKeywordHybridSearchable<>), typeof(ArangoHybridSearchable<>));
        return services;
    }
}
