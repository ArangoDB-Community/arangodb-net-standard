using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.MemoryStorage;
using Microsoft.KernelMemory.Search;

using System;

namespace ArangoDB.KernelMemory;

public static class DependencyRegistration
{
    public static KernelMemoryBuilder AddArangoMemory(
        this IServiceCollection services)
    {
        KernelMemoryBuilder builder = new(services);
        builder.WithArangoMemory();
        return builder;
    }

    public static KernelMemoryBuilder WithArangoMemory(
        this KernelMemoryBuilder builder)
    {
        builder.Services.AddSingleton<IMemoryDb, ArangoMemoryDb>();
        builder.Services.AddSingleton<ISearchClient, ArangoSearchClient>();
        builder
            .WithCustomMemoryDb<ArangoMemoryDb>()
            .WithCustomSearchClient<ArangoSearchClient>();
        return builder;
    }

    public static KernelMemoryBuilder WithArangoMemory(
        this KernelMemoryBuilder builder,
        string serviceKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(serviceKey);
        builder.Services.AddKeyedSingleton<IMemoryDb, ArangoMemoryDb>(serviceKey);
        builder.Services.AddKeyedSingleton<ISearchClient, ArangoSearchClient>(serviceKey);
        builder
            .WithCustomMemoryDb<ArangoMemoryDb>()
            .WithCustomSearchClient<ArangoSearchClient>();
        return builder;
    }
}
