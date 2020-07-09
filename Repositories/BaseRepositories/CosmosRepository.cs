using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.BaseEntities;

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

        protected virtual string DefaultFilterSql()
        {
            return $"SELECT * FROM c WHERE c.type='{CreatePartitionKey()}' ";
        }

        protected virtual string BuildFindOneByIdQuery(string id)
        {
            return $"{DefaultFilterSql()} AND c.id='{id}' ";
        }

        protected virtual string BuildFindItemByIdsQuery(params string[] ids)
        {
            var arrStr = $"({ids.Select(x => $"'{x}'").Aggregate((x, y) => $"{x},{y}")})";
            return $"{DefaultFilterSql()} AND c.id IN {arrStr}";
        }

        protected virtual string BuildSelectAllQuery(string queryString = null)
        {
            return $"{DefaultFilterSql()} " + queryString;
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

        public async Task<T> UpdateItemAsync(T item)
        {
            DefaultContainer.UpsertItemAsync<T>(item, new PartitionKey(CreatePartitionKey())).GetAwaiter().GetResult();
            return await QueryFindOneById(item.Id);
        }

        public async Task<T> InsertItemAsync(T item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.Meta ??= new Meta();
            item.Meta.CreatedAt = DateTime.UtcNow;
            var entity = await this.DefaultContainer.CreateItemAsync<T>(item, new PartitionKey(CreatePartitionKey()));
            return await QueryFindOneById(entity.Resource.Id);
        }
    }
}