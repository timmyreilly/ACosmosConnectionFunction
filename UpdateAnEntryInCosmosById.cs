
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
    public static class UpdateAnEntryInCosmosById
    {
        [FunctionName("UpdateAnEntryInCosmosById")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, 
        [CosmosDB(
                databaseName: "CheeseBurgerDatabase",
                collectionName: "CollectionOfCheeseburgers",
                ConnectionStringSetting = "CosmosDBConnection", 
                Id = "{Query.id}"
                )] CheeseBurger item,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (item is null) {
                log.LogInformation("burger not found"); 
            } else {
                log.LogInformation($"Found a cheeseburger!: {item.ToString()}"); 
            }

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            item.ItemName = name; 

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
