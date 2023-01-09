using DAL;
using DAL.Repositories;
using Models;

namespace AzureStorageRepo.Repos;

public interface IGameRepository : IAzureTableRepository<Game>
{
}
