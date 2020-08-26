using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Models.Paging;
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
            .Field(b => b.Name);

        public BlogRepository(CosmosDbContext cosmosDb) : base(cosmosDb.Container, nameof(Blog))
        {
        }

        public async Task<bool> DeleteAsync(UserInfo user, params string[] ids)
        {
            await DeleteItemAsync(user, ids);
            return true;
        }

        public async Task<List<Blog>> GetAllAsync(int? maxResultCount = null, string orderBy = null, bool isOrderAsc = true)
        {
            return await QueryAll(maxResultCount, orderBy, isOrderAsc);
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

        public async Task<PagingResult> PageIndexingItemsAsync(PagingRequest request)
        {
            var count = await QueryItemCount();
            var maxPage = Math.Ceiling((double)count / request.ItemsPerPage);
            var result = new PagingResult()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                MaxItemCount = count
            };

            if (request.CurrentPage <= maxPage)
            {
                var skipPages = request.CurrentPage > 1 ? request.CurrentPage - 1 : 0;
                result.Items = QueryPaging(skipPages, request.ItemsPerPage).GetAwaiter().GetResult().Select(x => x.ToIndexingModel());
            }

            return result;
        }

        public async Task<int> GetItemCount()
        {
            return await QueryItemCount();
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