using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;
public interface IAzureTableRepository<TEntity> where TEntity : class, ITableEntity, new()
{
    Task<Response> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
     Task<Response> InsertOrUpdate(TEntity entity, TableUpdateMode updateMode = TableUpdateMode.Merge, CancellationToken cancellationToken = default);
  
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
