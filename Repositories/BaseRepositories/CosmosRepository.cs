﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityDemo.Data.BaseEntities;
using UniversityDemo.Extensions;
using UniversityDemo.Identity;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public class CosmosRepository<T> where T : BaseEntity
    {
        protected CosmosRepository(Container defaultContainer)
        {
            DefaultContainer = defaultContainer;
        }
        //Install-Package Microsoft.Extensions.Caching.Cosmos -Version 1.0.0-preview
        protected QueryRequestOptions _requestOptions { get; set; } = new QueryRequestOptions();

        protected Container DefaultContainer { get; }

        protected virtual string GetPartitionKey()
        {
            return typeof(T).Name;
        }
        protected virtual QueryRequestOptions GetRequestOptions(T item = default)
        {
            return GetRequestOptions<T>(item);
        }

        protected virtual QueryRequestOptions GetRequestOptions<TEntity>(TEntity item = default) where TEntity : BaseEntity
        {
            return _requestOptions;
        }

        protected virtual string DefaultSql(int? maxResultCount = null)
        {
            return maxResultCount == null ? $"SELECT * FROM c WHERE " :
                                            $"SELECT TOP {maxResultCount.ToString()} * FROM c WHERE ";
        }

        protected virtual string DefaultFilterSql()
        {
            return $" ( c.meta.isDeleted = false OR (NOT IS_DEFINED(c.meta.isDeleted))) ";
        }

        protected virtual string OrderBySql(string field = null, bool isASC=true)
        {
            if (string.IsNullOrEmpty(field))
                return null;

            return $"ORDER BY c.{field} {GetOrderRule(isASC)}";
        }

        private string GetOrderRule(bool isASC = true)
        {
            return isASC ? " ASC" : " DESC";
        }
        
        protected virtual string AddPaginationSql(int skipPages = 0, int take = 10)
        {
            return $" OFFSET {(skipPages * take).ToString()} LIMIT {take}";
        }

        protected virtual string BuildItemCountQuery()
        {
            return $"SELECT VALUE COUNT(1) FROM c WHERE {DefaultFilterSql()}";
        }

        protected virtual string BuildPagingQuery(int skipPages = 0, int take = 10)
        {
            return $"{DefaultSql()}{DefaultFilterSql()}{AddPaginationSql(skipPages, take)}";
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

        protected virtual string BuildSelectAllQuery(int? maxResultCount = null, string orderBy = null, bool isOrderAsc = true)
        {
            return $"{DefaultSql(maxResultCount)} {DefaultFilterSql()} " 
                //+ $"{OrderBySql(orderBy, isOrderAsc)}"
                + $"{OrderBySql(orderBy, isOrderAsc)}"
                ;
        }

        protected FeedIterator<T> BuildDocumentQuery(string queryString)
        {
            return this.DefaultContainer.GetItemQueryIterator<T>(new QueryDefinition(queryString), null, _requestOptions);
        }

        protected async Task<List<T>> QueryAll(int? maxResultCount = null, string orderBy = null, bool isOrderAsc = true)
        {
            var queryStr = BuildSelectAllQuery(maxResultCount, orderBy, isOrderAsc);
            var query = BuildDocumentQuery(queryStr);
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        protected async Task<int> QueryItemCount()
        {
            var queryStr = BuildItemCountQuery();
            var query= this.DefaultContainer.GetItemQueryIterator<int>(new QueryDefinition(queryStr), null, _requestOptions);
            var result = await query.ReadNextAsync();
            return result.FirstOrDefault();
        }

        protected async Task<List<T>> QueryPaging(int skipPages = 0, int take = 10)
        {
            var queryStr = BuildPagingQuery(skipPages, take);
            var query = BuildDocumentQuery(queryStr);
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
            var queryStr = BuildFindItemByIdsQuery(ids);
            var query = BuildDocumentQuery(queryStr);
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

        public async Task<T> UpdateItemAsync(IUserInfo user, T item)
        {
            item.Meta.UpdatedAt = DateTime.UtcNow;
            item.Meta.UpdatedBy = user.Id;
            DefaultContainer.UpsertItemAsync<T>(item, _requestOptions.PartitionKey).GetAwaiter().GetResult();
            return await QueryFindOneById(item.Id);
        }

        public async Task<T> InsertItemAsync(IUserInfo user, T item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.Meta.CreatedAt = DateTime.UtcNow;
            item.Meta.CreatedBy = user.Id;
            item.Type = GetPartitionKey();
            var entity = await this.DefaultContainer.CreateItemAsync<T>(item, _requestOptions.PartitionKey);
            return await QueryFindOneById(entity.Resource.Id);
        }

        public async Task DeleteItemAsync(IUserInfo user, string id)
        {
            var item = await QueryFindOneById(id);
            if (item == null)
                return;
            item.Meta.UpdatedAt = DateTime.UtcNow;
            item.Meta.UpdatedBy = user.Id;
            item.Meta.IsDeleted = true;
            await DefaultContainer.UpsertItemAsync<T>(item, _requestOptions.PartitionKey);
        }

        public async Task DeleteItemAsync(IUserInfo user, params string[] ids)
        {
            foreach (var id in ids)
            {
                await DeleteItemAsync(user, id);
            }
        }

        public async Task RemoveItemAsync(string id)
        {
            await DefaultContainer.DeleteItemAsync<T>(id, _requestOptions.PartitionKey.GetValueOrDefault());
        }

    }
}