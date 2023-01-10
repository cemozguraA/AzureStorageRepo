using Azure.Data.Tables;
using Azure;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;

namespace DAL;
public class AzureBaseRepository<TEntity> : IAzureTableRepository<TEntity> where TEntity : class, ITableEntity, new()
{
    private readonly IAzureTableClient _azureTableClient;
    public TableClient TableClient { get { return _tableClient; } }
    TableClient _tableClient;

    public AzureBaseRepository(IAzureTableClient azureTableClient)
    {
        _azureTableClient = azureTableClient;
        _tableClient = _azureTableClient.TableServiceClient.GetTableClient(typeof(TEntity).Name);
        _tableClient.CreateIfNotExists();
    }


    public async Task<Response> DeleteAsync(string partitionKey, string rowKey, CancellationToken cancellationToken = default)
    {
        return await TableClient.DeleteEntityAsync(partitionKey, rowKey, ETag.All, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicat, CancellationToken cancellationToken = default)
    {
        var getAll = await GetListAsync(predicat, cancellationToken);
        return getAll.FirstOrDefault();
    }
    public async Task<TEntity?> GetAsync(string partitionKey, string rowKey, CancellationToken cancellationToken = default)
    {
        var entity = await TableClient.GetEntityIfExistsAsync<TEntity>(partitionKey, rowKey, cancellationToken: cancellationToken);
        return entity.Value;
    }
    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await TableClient.QueryAsync<TEntity>(filter: "", cancellationToken: cancellationToken).ToListAsync();

    }
    public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicat, CancellationToken cancellationToken = default)
    {
        return await TableClient.QueryAsync<TEntity>(predicat, cancellationToken: cancellationToken).ToListAsync();
    }
    public async Task<Tuple<string, IList<TEntity>>> GetListAsync(Expression<Func<TEntity, bool>> predicat, int pageSize, string continuationToken, CancellationToken cancellationToken = default)
    {
        var pageing = TableClient.QueryAsync<TEntity>(predicat, pageSize, cancellationToken: cancellationToken);
        await foreach (var page in pageing.AsPages(continuationToken))
        {
            return Tuple.Create<string, IList<TEntity>>(page.ContinuationToken, page.Values.ToList());
        }
        return null;
    }
    public async Task<Response> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await TableClient.AddEntityAsync(entity);

    }

    public async Task<Response> UpdateAsync(TEntity entity, TableUpdateMode tableUpdateMode, CancellationToken cancellationToken = default)
    {
        return await TableClient.UpdateEntityAsync(entity, ETag.All, mode: tableUpdateMode, cancellationToken: cancellationToken);
    }

    public async Task<Azure.Response<IReadOnlyList<Response>>> BulkAsync(IList<TEntity> entityList, TableTransactionActionType actionType = TableTransactionActionType.Add, CancellationToken cancellationToken = default)
    {
        List<TableTransactionAction> addEntitiesBatch = new List<TableTransactionAction>();
        addEntitiesBatch.AddRange(entityList.Select(e => new TableTransactionAction(actionType, e)));
        return await TableClient.SubmitTransactionAsync(addEntitiesBatch, cancellationToken);
    }

    public async Task<Azure.Response<IReadOnlyList<Response>>> BulkAsync(IList<TableTransactionAction> actionList, CancellationToken cancellationToken = default)
    {
        return await TableClient.SubmitTransactionAsync(actionList, cancellationToken);
    }
     public async Task<Response> InsertOrUpdate(TEntity entity, TableUpdateMode updateMode = TableUpdateMode.Merge, CancellationToken cancellationToken = default)
    {
        return await TableClient.UpsertEntityAsync(entity, updateMode, cancellationToken);
    }
}
