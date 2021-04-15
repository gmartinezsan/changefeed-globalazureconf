using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebStore.ChangeFeedFunctions
{

    /// 
    /// Función que ilustra el uso del Cosmos DB Trigger para monitoreo de cambios en un container de CosmosDB
    ///
    public class ReplicateToShopContainer
    {
        private static readonly CosmosClient _client;

        static ReplicateToShopContainer()
        {
            var connStr = Environment.GetEnvironmentVariable("CosmosDBConnectionString", EnvironmentVariableTarget.Process);
            _client = new CosmosClient(connStr);
        }

        [FunctionName("ReplicateToShopContainer")]
        public static async Task ReplicateCart(
            [CosmosDBTrigger(
                databaseName: "WebStore",
                collectionName: "CustomerContainer",
                ConnectionStringSetting = "CosmosDBConnectionString",
                LeaseCollectionName = "Lease",
                LeaseCollectionPrefix = "ReplicateShop",
                CreateLeaseCollectionIfNotExists = true,
                StartFromBeginning = true)]
            IReadOnlyList<Document> input,
            ILogger logger)
        {
            var container = _client.GetContainer("WebStore", "ShopContainer");
            foreach (var document in input)
            {
                try
                {
                    document.SetPropertyValue("ShopId", "MEX");
                    await container.UpsertItemAsync(document);
                    logger.LogWarning($"Upserted document id {document.Id} in Shop container");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error processing change for document id {document.Id}: {ex.Message}");
                }
            }
        }
    }
}