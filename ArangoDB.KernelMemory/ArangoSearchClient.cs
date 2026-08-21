using Microsoft.KernelMemory;
using Microsoft.KernelMemory.Context;
using Microsoft.KernelMemory.Search;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDB.KernelMemory;

public sealed class ArangoSearchClient : ISearchClient
{
    public Task<MemoryAnswer> AskAsync(string index, string question, ICollection<MemoryFilter> filters = null, double minRelevance = 0, IContext context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<MemoryAnswer> AskStreamingAsync(string index, string question, ICollection<MemoryFilter> filters = null, double minRelevance = 0, IContext context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListIndexesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SearchResult> SearchAsync(string index, string query, ICollection<MemoryFilter> filters = null, double minRelevance = 0, int limit = -1, IContext context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
