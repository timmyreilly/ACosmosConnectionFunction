# Going to go in and out on CosmosDB Hopefully...

1. Create new HTTP Trigger from Azure Function extensions. 
2. Try to talk to Cosmos Attribute... 
3. Add some app setting strings to local.settings.json: 
*it needs to be "CosmosDBConnection" for the setting name*

```json
    "CosmosDBConnection": "AccountEndpoint=https://my-db.documents.azure.com:443/;AccountKey=fjAfOKqbDhKJoJoCSE5XhBUNnECQi2B97nnk0xK44mopXaWnhlMT4PHjIlEMpVvQVHmrbCBIE1mT6Vhw==;",
    "CosmosEndpoint": "https://kosmos-staging.documents.azure.com:443/",
    "CosmosKey": "6vLRnngSECRETSECRETYccxAxwI9ds04DAqySA74afW4b4YMdjJlqIyxPQM4ON3Rocd1bSJj6r9g2r1hw==",
```

4. Add Cosmos Extension by editing .csproj file: 

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Sdk.Functions" Version="1.0.22" />
    <PackageReference Include="Microsoft.Azure.Webjobs.Extensions.CosmosDB" Version="3.0.1"/>
  </ItemGroup>
```

5. Run a restore or try to build. 
6. Then you can the CosmosDB attribute to your function: 

```csharp
public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            [CosmosDB()],
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ... 

```

