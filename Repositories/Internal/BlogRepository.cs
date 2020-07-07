using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Models;
using UniversityDemo.Repositories.BaseRepositories;

namespace UniversityDemo.Repositories.Internal
{
    public class BlogRepository : CosmosRepository<Blog>, IBlogRepository
    {
        private CosmosDbService cosmosDb { get; }

        public BlogRepository(CosmosDbService cosmosDb):base(cosmosDb.Container)
        {
            this.cosmosDb = cosmosDb;
        }

        public async Task<bool> DeleteAsync(params string[] ids)
        {
            foreach (var id in ids)
            {
                await cosmosDb.Container.DeleteItemAsync<Blog>(id, new PartitionKey(nameof(Blog)));
            }

            return true;
        }

        public async Task<List<Blog>> FindAllAsync()
        {
            return await QueryAll();
        }

        public Task<Blog> FindOneByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> InsertAsync(Blog item)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> UpdateAsync(Blog item)
        {
            throw new NotImplementedException();
        }
    }
}