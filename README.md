# AzureStorageRepo
New AzureStorage Repo and Injection Extension


### NugetPackage
[https://www.nuget.org/packages/Azure.Data.Tables/](https://www.nuget.org/packages/Azure.Data.Tables/)

Azure storage structure has changed and repository pattern suitable for its current structure.

## Example
### Set Program.cs
```csharp
       //you can TableClientOptions
       services.AddTableStorageServices(x =>
       {
           x.AzureTableConnectionString = builder.Configuration["BlobService:ConnectionString"];
       });
```
### Create Your Entity 
```csharp
       public class Game : ITableEntity
       {
           public string Name { get; set; }
           public string Description { get; set; }
           public long Price { get; set; }
           public string PartitionKey { get ; set ; }
           public string RowKey { get ; set ; }
           public DateTimeOffset? Timestamp { get ; set ; }
           public ETag ETag { get ; set ; }
       }
```
### Create Your Repostiory
#### Interface
```csharp
       public interface IGameRepository : IAzureTableRepository<Game>
       {
       }
```
#### Class
```csharp
       public class GameRepository : AzureBaseRepository<Game>, IGameRepository
       {
           public GameRepository(IAzureTableClient azureTableClient) : base(azureTableClient)
           {
           }
       }
```
