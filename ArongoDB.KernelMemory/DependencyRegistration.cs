using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.MemoryStorage;
using Microsoft.KernelMemory.Search;

using System;

namespace ArongoDB.KernelMemory;

public static class DependencyRegistration
{
    public static KernelMemoryBuilder AddArongoMemory(
        this IServiceCollection services)
    {
        KernelMemoryBuilder builder = new(services);
        builder.WithArongoMemory();
        return builder;
    }

    public static KernelMemoryBuilder WithArongoMemory(
        this KernelMemoryBuilder builder)
    {
        builder.Services.AddSingleton<IMemoryDb, ArongoMemoryDb>();
        builder.Services.AddSingleton<ISearchClient, ArongoSearchClient>();
        builder
            .WithCustomMemoryDb<ArongoMemoryDb>()
            .WithCustomSearchClient<ArongoSearchClient>();
        return builder;
    }

    public static KernelMemoryBuilder WithArongoMemory(
        this KernelMemoryBuilder builder,
        string serviceKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(serviceKey);
        builder.Services.AddKeyedSingleton<IMemoryDb, ArongoMemoryDb>(serviceKey);
        builder.Services.AddKeyedSingleton<ISearchClient, ArongoSearchClient>(serviceKey);
        builder
            .WithCustomMemoryDb<ArongoMemoryDb>()
            .WithCustomSearchClient<ArongoSearchClient>();
        return builder;
    }
}
