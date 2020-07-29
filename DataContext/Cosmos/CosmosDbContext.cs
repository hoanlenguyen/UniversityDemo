using Microsoft.Azure.Cosmos;

namespace UniversityDemo.DataContext.Cosmos
{
    public class CosmosDbContext
    {
        public Container Container { get; private set; }

        public CosmosDbContext(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            Container = dbClient.GetContainer(databaseName, containerName);
        }
    }
}