using Microsoft.Azure.Cosmos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories.Internal
{
    public class PostRepository : CosmosRepository<Post>, IPostRepository
    {
        private CosmosDbService cosmosDb { get; }

        public PostRepository(CosmosDbService cosmosDb) : base(cosmosDb.Container)
        {
            this.cosmosDb = cosmosDb;
        }

        private static QueryDefinition<Post> IndexingDefinition =>
        QueryDefinition<Post>.Select
            .Field(q => q.Id)
            .Field(q => q.Title)
            .Field(q=>q.CoverImagePath)
            .Field(q=>q.Meta)
            .Field(q=>q.BlogId)
            .Field(q => q.Views);

        public async Task<bool> DeleteAsync(UserInfo user, params string[] ids)
        {
            await DeleteItemAsync(user, ids);
            return true;
        }

        public async Task<List<Post>> FindAllAsync()
        {
            return await QueryAll();
        }

        public async Task<Post> FindOneByIdAsync(string id)
        {
            return await QueryFindOneById(id);
        }

        public async Task<List<Post>> FindByIdsAsync(params string[] ids)
        {
            return await QueryFindItemsByIds(ids);
        }

        public async Task<Post> InsertAsync(UserInfo user, Post item)
        {
            return await InsertItemAsync(user, item);
        }

        public async Task<Post> UpdateAsync(UserInfo user, Post item)
        {
            return await UpdateItemAsync(user, item);
        }

        public async Task<List<Post>> FindIndexingAsync(string blogId = null, CancellationToken token = default)
        {
            return await QueryIndexing(blogId).FetchAsync(token);
        }

        FeedIterator<Post> QueryIndexing(string blogId=null)
        {
            var queryString = $"SELECT {IndexingDefinition.Build()} FROM c WHERE {DefaultFilterSql()} " +
                             (string.IsNullOrEmpty(blogId)? "": $"AND c.blogId='{blogId}'");

            var query = BuildDocumentQuery(queryString);
            return query;
        }

        #region Cosmos

        protected override string CreatePartitionKey()
        {
            return nameof(Post);
        }

        #endregion Cosmos
    }
}