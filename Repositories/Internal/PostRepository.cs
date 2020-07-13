using System;
using System.Collections.Generic;
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

        #region Cosmos

        protected override string CreatePartitionKey()
        {
            return nameof(Post);
        }

        #endregion Cosmos
    }
}