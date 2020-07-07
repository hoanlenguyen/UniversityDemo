using Microsoft.Azure.Cosmos;

namespace UniversityDemo.DataContext.Cosmos
{
    public class CosmosDbService
    {
        public Container Container { get; private set; }

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            Container = dbClient.GetContainer(databaseName, containerName);
        }
    }
}