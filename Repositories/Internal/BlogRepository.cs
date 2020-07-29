using Microsoft.Azure.Cosmos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories.Internal
{
    public class BlogRepository : CosmosPartitionRepository<Blog>, IBlogRepository
    {
        //private static QueryDefinition<Blog> PublicDefinition =>
        //QueryDefinition<Blog>.Select
        //    .Field(b => b.Id)
        //    .Field(b => b.Url);

        private static QueryDefinition<Blog> IndexingDefinition =>
        QueryDefinition<Blog>.Select
            .Field(b => b.Id)
            .Field(b => b.Url);

        public BlogRepository(CosmosDbContext cosmosDb) : base(cosmosDb.Container, nameof(Blog))
        {
        }

        public async Task<bool> DeleteAsync(UserInfo user, params string[] ids)
        {
            await DeleteItemAsync(user, ids);
            return true;
        }

        public async Task<List<Blog>> GetAllAsync(int? maxResultCount = null)
        {
            return await QueryAll(maxResultCount);
        }

        public async Task<Blog> FindOneByIdAsync(string id)
        {
            return await QueryFindOneById(id);
        }

        public async Task<List<Blog>> FindByIdsAsync(params string[] ids)
        {
            return await QueryFindItemsByIds(ids);
        }

        public async Task<Blog> InsertAsync(UserInfo user, Blog item)
        {
            return await InsertItemAsync(user, item);
        }

        public async Task<Blog> UpdateAsync(UserInfo user, Blog item)
        {
            return await UpdateItemAsync(user, item);
        }

        public async Task<IEnumerable> PageIndexingItemsAsync(int skipPages = 0, int take = 10)
        {
            var blogs = QueryPaging(skipPages, take).GetAwaiter().GetResult();
            return blogs.Select(x => x.ToIndexingModel());
        }

        //public async Task<IEnumerable> FindIndexingAsync(CancellationToken token = default)
        //{
        //    return await QueryIndexing().FetchAsync(token);
        //}

        //FeedIterator<Blog> QueryIndexing()
        //{
        //    var queryString = $"SELECT {IndexingDefinition.Build()} FROM c WHERE {DefaultFilterSql()} ";

        //    var query = BuildDocumentQuery(queryString);
        //    return query;
        //}

    }
}