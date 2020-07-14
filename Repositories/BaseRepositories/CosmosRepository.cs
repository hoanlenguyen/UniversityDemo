using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.BaseEntities;
using UniversityDemo.Identity;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public class CosmosRepository<T> where T : BaseEntity
    {
        protected Container DefaultContainer { get; }

        protected CosmosRepository(Container defaultContainer)
        {
            DefaultContainer = defaultContainer;
        }
        protected virtual string CreatePartitionKey()
        {
            return nameof(T);
        }
        protected virtual string DefaultSql()
        {
            return $"SELECT * FROM c WHERE ";
        }
        protected virtual string DefaultFilterSql()
        {
            return $" c.type='{CreatePartitionKey()}' " +
                    $"AND ( c.meta.isDeleted=false OR (NOT IS_DEFINED(c.meta.isDeleted))) ";
        }

        protected virtual string BuildFindOneByIdQuery(string id)
        {
            return $"{DefaultSql()}{DefaultFilterSql()} AND c.id='{id}' ";
        }

        protected virtual string BuildFindItemByIdsQuery(params string[] ids)
        {
            var arrStr = $"({ids.Select(x => $"'{x}'").Aggregate((x, y) => $"{x},{y}")})";
            return $"{DefaultSql()}{DefaultFilterSql()} AND c.id IN {arrStr}";
        }

        protected virtual string BuildSelectAllQuery(string queryString = null)
        {
            return $"{DefaultSql()}{DefaultFilterSql()} " + queryString;
        }

        protected FeedIterator<T> BuildDocumentQuery(string queryString)
        {
            return this.DefaultContainer.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        }

        protected async Task<List<T>> QueryAll()
        {
            var query = BuildDocumentQuery(BuildSelectAllQuery());
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        protected async Task<List<T>> QueryFindItemsByIds(params string[] ids)
        {
            var query = BuildDocumentQuery(BuildFindItemByIdsQuery(ids));
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        protected async Task<T> QueryFindOneById(string id, CancellationToken token = default)
        {
            var query = BuildDocumentQuery(BuildFindOneByIdQuery(id));
            var result = await query.FirstOrDefaultAsync(token);

            return result;
        }

        public async Task<T> UpdateItemAsync(UserInfo user, T item)
        {
            item.Meta.UpdatedAt = DateTime.UtcNow;
            item.Meta.UpdatedBy = user.Id;
            item.Type = CreatePartitionKey();
            DefaultContainer.UpsertItemAsync<T>(item, new PartitionKey(CreatePartitionKey())).GetAwaiter().GetResult();
            return await QueryFindOneById(item.Id);
        }

        public async Task<T> InsertItemAsync(UserInfo user, T item)
        {
            item.Id = Guid.NewGuid().ToString();
            //item.Meta ??= new Meta();
            item.Meta.CreatedAt = DateTime.UtcNow;
            item.Meta.CreatedBy = user.Id;
            item.Type = CreatePartitionKey();
            var entity = await this.DefaultContainer.CreateItemAsync<T>(item, new PartitionKey(CreatePartitionKey()));
            return await QueryFindOneById(entity.Resource.Id);
        }

        public async Task DeleteItemByIdAsync(UserInfo user, string id)
        {
            var item = await QueryFindOneById(id);
            if (item == null)
                return;
            item.Meta.UpdatedAt = DateTime.UtcNow;
            item.Meta.UpdatedBy = user.Id;
            item.Meta.IsDeleted = true;
            await DefaultContainer.UpsertItemAsync<T>(item, new PartitionKey(CreatePartitionKey()));
        }

        public async Task DeleteItemAsync(UserInfo user, T item)
        {
            item.Meta.UpdatedAt = DateTime.UtcNow;
            item.Meta.UpdatedBy = user.Id;
            item.Meta.IsDeleted = true;
            await DefaultContainer.UpsertItemAsync<T>(item, new PartitionKey(CreatePartitionKey()));
        }

        public async Task DeleteItemAsync(UserInfo user, params string[] ids)
        {
            foreach (var id in ids)
            {
                await DeleteItemByIdAsync(user, id);
            }
        }

        public async Task RemoveItemAsync(T item)
        {
            await DefaultContainer.DeleteItemAsync<T>(item.Id, new PartitionKey(CreatePartitionKey()));
        }
    }
}