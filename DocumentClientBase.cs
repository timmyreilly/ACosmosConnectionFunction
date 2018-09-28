
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Azure.Documents.Client; 
using Microsoft.Azure.Documents.Linq; 

namespace Company.Function
{
    public static class DocumentClientBase
    {
        [FunctionName("DocumentClientBase")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, 
            [CosmosDB(
                databaseName: "CheeseBurgerDatabase",
                collectionName: "CollectionOfCheeseburgers",
                ConnectionStringSetting = "CosmosDBConnection"
            )] DocumentClient client, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var searchterm = req.Query["searchterm"];
            if (string.IsNullOrWhiteSpace(searchterm))
            {
                return (ActionResult)new NotFoundResult();
            }

            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("CheeseBurgerDatabase", "CollectionOfCheeseburgers");

            log.LogInformation($"Searching for: {searchterm}");

            IDocumentQuery<CheeseBurger> query = client.CreateDocumentQuery<CheeseBurger>(collectionUri)
                .Where(p => p.ItemName.Contains(searchterm))
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                foreach (CheeseBurger result in await query.ExecuteNextAsync())
                {
                    log.LogInformation(result.ToString());
                }
            }
            return new OkResult();
        }
    }
}
