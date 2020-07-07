using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
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

        protected virtual string DefaultFilterSql()
        {
            return $" AND c.type='{nameof(T)}' ";
        }

        protected string BuildSelectAllQuery(string queryString = null)
        {
            return $"SELECT * FROM c WHERE {DefaultFilterSql()} " + queryString;
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
    }
}