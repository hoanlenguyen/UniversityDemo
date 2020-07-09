using Microsoft.Azure.Cosmos;
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

        public BlogRepository(CosmosDbService cosmosDb) : base(cosmosDb.Container)
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

        public async Task<Blog> FindOneByIdAsync(string id)
        {
            return await QueryFindOneById(id);
        }

        public async Task<List<Blog>> FindByIdsAsync(params string[] ids)
        {
            return await QueryFindItemsByIds(ids);
        }

        public async Task<Blog> InsertAsync(Blog item)
        {
            return await InsertItemAsync(item);
        }

        public async Task<Blog> UpdateAsync(Blog item)
        {
            return await UpdateItemAsync(item);
        }

        #region Cosmos

        protected override string CreatePartitionKey()
        {
            return nameof(Blog);
        }

        #endregion Cosmos
    }
}