# Going to go in and out on CosmosDB Hopefully...

1. Create new HTTP Trigger from Azure Function extensions. 
2. Try to talk to Cosmos Attribute... 
3. Add some app setting strings to local.settings.json: 
*it needs to be "CosmosDBConnection" for the setting name*

```json
    "CosmosDBConnection": "AccountEndpoint=https://my-db.documents.azure.com:443/;AccountKey=fjAfOKqbDhKJoJoCSE5XhBUNnECQi2B97nnk0xK44mopXaWnhlMT4PHjIlEMpVvQVHmrbCBIE1mT6Vhw==;"
```

4. Add Cosmos Extension by editing .csproj file: 
As of September 27th, 2018 I could only get this to build with the Beta...  

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Sdk.Functions" Version="1.0.22" />
    <PackageReference Include="Microsoft.Azure.Webjobs.Extensions.CosmosDB" Version="3.0.0-beta7"/>
  </ItemGroup>
```

5. Run a restore or try to build. 
6. Then you can the CosmosDB attribute to your function: 

```csharp
public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            [CosmosDB(
                databaseName: "CheeseBurgerDatabase",
                collectionName: "CollectionOfCheeseburgers",
                ConnectionStringSetting = "CosmosDBConnection"
                )] ICollector<CheeseBurger> item,
             ILogger log)
        {
            ... 

```


Okay, there are going to be dozen of different permutations for how to talk to Cosmos from your function. 

First we need to distinguish between Binding to the Cosmos Attribute and Binding to the Document Client. 

Then we need to visit how Functions can read, update, insert, and delete and entry, and entries. 

So that's

2 types of connection patterns... * 4 types of operations * 2 (singular and plural) = 16 total functions: 

### Using the Document Client: 
1. Create an Entry
2. Update an Entry 
3. Delete an Entry
4. Read an Entry

5. Create Entries
6. Update Entries
7. Delete Entries
8. Read Entries

### Binding to a "Cosmos Document", I guess that's what we're doing. 

9. Create an Entry - that doesn't make sense
10. Update an Entry 
11. Delete an Entry - does that make sense? 
12. Read an Entry 

13. Create Entries 
14. Update Entries
15. Delete Entries
16. Read Entries 

### Proposed Function Names for DocumentClient: 
1. DocumentClientCreateEntry
2. DocumentClientUpdateEntry
3. DocumentClientDeleteAnEntry
4. DocumentClientReadAnEntry

I'm also looking at these and thinking of all the permutations of return. The `OkActionResult`, from `IActionResult`, or just a string, or elaborate Http responses? 

5. DocumentClientCreateEntries
6. DocumentClientUpdateEntries

Inside of update, we can also have update, or upsert. +1 

7. DocumentClientDeleteEntries
8. DocumentClientReadEntries

### Proposed Function Names for "CosmosDocBinding" 

9. CosmosDocBindingCreateAnEntry - this still doesn't make sense to me. 
10. CosmosDocBindingUpdateAnEntry 
11. CosmosDocBindingDeleteAnEntry - *call and destroy lolz*
12. CosmosDocBindingReadAnEntry
13. CosmosDocBindingCreateEntries - why? why wouldn't we always use our document client? 
14. CosmosDocBindingUpdateEntries - could be nice with docs you change regularly 
15. CosmosDocBindingDeleteEntries - *call and destroy many lololz*
16. CosmosDocBindingReadEntries - Why would I bind and read like this? 




