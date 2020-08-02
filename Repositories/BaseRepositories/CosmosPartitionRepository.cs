using Microsoft.Azure.Cosmos;
using UniversityDemo.BaseEntities;

namespace UniversityDemo.Repositories.BaseRepositories
{
    public class CosmosPartitionRepository<T> : CosmosRepository<T> where T : BaseEntity
    {
        protected CosmosPartitionRepository(Container defaultContainer) : this(defaultContainer, typeof(T).Name)
        {
        }

        protected CosmosPartitionRepository(Container defaultContainer, string partitionId) : base(defaultContainer)
        {
            if (!string.IsNullOrEmpty(partitionId))
            {
                PartitionId = partitionId;

                _requestOptions = new QueryRequestOptions { PartitionKey = new PartitionKey(PartitionId), MaxItemCount = 100, };
            }
        }

        private string PartitionId { get; set; }

        protected override QueryRequestOptions GetRequestOptions<TU>(TU item = default)
        {
            return _requestOptions;
        }

        protected override string GetPartitionKey()
        {
            return PartitionId ?? typeof(T).Name;
        }
    }
}