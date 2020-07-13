﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Identity;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories.Internal
{
    public class BlogRepository : CosmosRepository<Blog>, IBlogRepository
    {
        private CosmosDbService cosmosDb { get; }

        public BlogRepository(CosmosDbService cosmosDb) : base(cosmosDb.Container)
        {
            this.cosmosDb = cosmosDb;
        }

        public async Task<bool> DeleteAsync(UserInfo user, params string[] ids)
        {
            await DeleteItemAsync(user, ids);
            return true;
        }

        public async Task<List<Blog>> FindAllAsync()
        {
            return await QueryAll();
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

        #region Cosmos

        protected override string CreatePartitionKey()
        {
            return nameof(Blog);
        }

        #endregion Cosmos
    }
}