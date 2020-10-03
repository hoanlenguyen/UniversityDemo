using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Data.Models;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Models.Paging;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories.Internal
{
    public class PostRepository : CosmosPartitionRepository<Post>, IPostRepository
    {
        public PostRepository(CosmosDbContext cosmosDb) : base(cosmosDb.Container, nameof(Post))
        {
        }

        private static QueryDefinition<Post> IndexingDefinition =>
        QueryDefinition<Post>.Select
            .Field(q => q.Id)
            .Field(q => q.Title)
            .Field(q => q.CoverImagePath)
            .Field(q => q.Meta)
            .Field(q => q.BlogId)
            .Field(q => q.Views);

        public async Task<bool> DeleteAsync(IUserInfo user, params string[] ids)
        {
            await DeleteItemAsync(user, ids);
            return true;
        }

        public async Task<List<Post>> GetAllAsync(int? maxResultCount = null)
        {
            return await QueryAll(maxResultCount);
        }

        public async Task<Post> FindOneByIdAsync(string id)
        {
            return await QueryFindOneById(id);
        }

        public async Task<List<Post>> FindByIdsAsync(params string[] ids)
        {
            return await QueryFindItemsByIds(ids);
        }

        public async Task<Post> InsertAsync(IUserInfo user, Post item)
        {
            return await InsertItemAsync(user, item);
        }

        public async Task<Post> UpdateAsync(IUserInfo user, Post item)
        {
            return await UpdateItemAsync(user, item);
        }

        public async Task<List<Post>> FindIndexingAsync(string blogId = null, CancellationToken token = default)
        {
            return await QueryIndexing(blogId).FetchAsync(token);
        }

        private FeedIterator<Post> QueryIndexing(string blogId = null)
        {
            var queryString = $"SELECT {IndexingDefinition.Build()} FROM c WHERE {DefaultFilterSql()} " +
                             (string.IsNullOrEmpty(blogId) ? "" : $"AND c.blogId='{blogId}'");

            var query = BuildDocumentQuery(queryString);
            return query;
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
    }
}