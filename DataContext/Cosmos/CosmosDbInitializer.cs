//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace UniversityDemo.DataContext.Cosmos
//{
//    public static class CosmosDbInitializer
//    {
//        public static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
//        {
//            string databaseName = configurationSection.GetSection("DatabaseName").Value;
//            string containerName = configurationSection.GetSection("ContainerName").Value;
//            string account = configurationSection.GetSection("Account").Value;
//            string key = configurationSection.GetSection("Key").Value;
//            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
//            CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
//            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
//            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/type");

//            return cosmosDbService;
//        }
//    }
//}