using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public static class CosmosHelper
    {
        public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, CancellationToken token = default)
        {
            return (await FetchAsync(query, token)).FirstOrDefault();
        }

        public static Task<List<TU>> FetchAsync<TU>(this IQueryable<TU> query, CancellationToken token = default)
        {
            return FetchAsync(query.ToFeedIterator(), token);
        }

        public static async Task<List<TU>> FetchAsync<TU>(this FeedIterator<TU> query, CancellationToken token = default)
        {
            var results = new List<TU>();
            while (query.HasMoreResults)
                results.AddRange(await query.ReadNextAsync(token));
            return results.ToList();
        }

        public static async Task<TU> FirstOrDefaultAsync<TU>(this FeedIterator<TU> query, CancellationToken token = default)
        {
            return (await query.ReadNextAsync(token)).FirstOrDefault();
        }
    }
}