using Microsoft.KernelMemory;
using Microsoft.KernelMemory.MemoryStorage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDB.KernelMemory;

public sealed class ArangoMemoryDb : IMemoryDb
{
    public Task CreateIndexAsync(
        string index,
        int vectorSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(
        string index,
        MemoryRecord record,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIndexAsync(
        string index,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<MemoryRecord> GetListAsync(
        string index,
        ICollection<MemoryFilter> filters = null,
        int limit = 1,
        bool withEmbeddings = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<(MemoryRecord, double)> GetSimilarListAsync(
        string index,
        string text,
        ICollection<MemoryFilter> filters = null,
        double minRelevance = 0,
        int limit = 1,
        bool withEmbeddings = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpsertAsync(
        string index,
        MemoryRecord record,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
