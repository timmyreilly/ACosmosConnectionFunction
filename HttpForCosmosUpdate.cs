
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

namespace Company.Function
{
    public static class HttpForCosmosUpdate
    {
        [FunctionName("HttpForCosmosUpdate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            [CosmosDB(
                databaseName: "CheeseBurgerDatabase",
                collectionName: "CollectionOfCheeseburgers",
                ConnectionStringSetting = "CosmosDBConnection"
                )] ICollector<ItemLookup> item,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            ItemLookup i = new ItemLookup();
            i.ItemName = name; 
            item.Add(i);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public class ItemLookup
        {
            public ItemLookup()
            {
                this.id = Guid.NewGuid().ToString();
            }
            public string ItemName { get; set; }
            public string id { get; set; }
        }
    }
}
