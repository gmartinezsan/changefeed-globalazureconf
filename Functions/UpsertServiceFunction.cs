using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Webstore.Functions.Models;

namespace WebStore.Functions
{
    public static class UpsertServiceFunction
    {

        /// 
        /// Funcion para ilustrar el uso de Queue Trigger y Cosmos Db Output Binding
        ///
        [FunctionName("UpsertServiceFunction")]
        public static void Run([QueueTrigger("webstorecustomers-queue", Connection = "QueueConnectionString")] Customer customerMessage,
        [CosmosDB(
        databaseName: "WebStore",
        collectionName: "CustomerContainer",
        ConnectionStringSetting = "CosmosDBConnectionString")]out Customer customerDocument,
        ILogger log)
        {
            // Cualquier transformacion que sea necesaria hacerla  aqui
            customerMessage.Name = customerMessage.Name.ToUpperInvariant();
            // id para Address 
            customerMessage.Address.Id = Guid.NewGuid().ToString();
            customerDocument = customerMessage;

            log.LogInformation($"C# Queue trigger function inserted one document.");
            log.LogInformation($"C# Queue trigger function processed: {customerMessage}");
        }
    }
}
