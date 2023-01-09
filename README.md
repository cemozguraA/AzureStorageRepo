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

#### Use Any Method You Want

```csharp
      var datas = await _gameRepository.GetAllAsync();
```



```csharp
       public interface IAzureTableRepository<TEntity> where TEntity : class, ITableEntity, new()
       {
           Task<Response> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
           Task<Response<IReadOnlyList<Response>>> BulkAsync(IList<TEntity> entityList, TableTransactionActionType actionType = TableTransactionActionType.Add, CancellationToken cancellationToken = default);
           Task<Response<IReadOnlyList<Response>>> BulkAsync(IList<TableTransactionAction> actionList, CancellationToken cancellationToken = default);
           Task<Response> UpdateAsync(TEntity entity, TableUpdateMode tableUpdateMode, CancellationToken cancellationToken = default);
           Task<Response> DeleteAsync(string partitionKey, string rowKey, CancellationToken cancellationToken = default);
           Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
           Task<TEntity?> GetAsync(string partitionKey, string rowKey, CancellationToken cancellationToken = default);
           Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
           Task<Tuple<string, IList<TEntity>>> GetListAsync(Expression<Func<TEntity, bool>> predicat, int pageSize, string continuationToken, CancellationToken cancellationToken = default);
           Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicat, CancellationToken cancellationToken = default);
       
       
       }
```
