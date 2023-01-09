using DAL;
using Models;

namespace AzureStorageRepo.Repos;

public class GameRepository : AzureBaseRepository<Game>, IGameRepository
{
    public GameRepository(IAzureTableClient azureTableClient) : base(azureTableClient)
    {
    }
}
